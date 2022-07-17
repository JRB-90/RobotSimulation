using Castle.Windsor;
using FluentAssertions;
using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Linkages;
using JSim.Core.Maths;
using JSim.Core.SceneGraph;
using Xunit;

namespace SceneGraphTests.TreeHelpers
{
    public class PositionableTests
    {
        const double eps = 0.0000001;

        [Fact]
        public void Can_Linkage_Pass_Positionable_Checks()
        {
            IWindsorContainer container = SceneGraphHelpers.BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;
            ISceneEntity entity1 = scene.Root.CreateNewEntity();
            ILinkageContainer linkages = entity1.LinkageContainer;

            StandardPositionableChecks(linkages.BaseLinkage);
        }

        private void StandardPositionableChecks(IPositionable positionable)
        {
            var t1 = new Transform3D(1, 2, 3, 4, 5, 6);

            var monitor = positionable.Monitor();
            positionable.WorldFrame.SetTransform(t1);
            Utils.AreApproxTheSame(positionable.WorldFrame.GetTransformCopy(), t1).Should().BeTrue();
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.LocalFrame.SetTransform(t1);
            Utils.AreApproxTheSame(positionable.WorldFrame.GetTransformCopy(), t1).Should().BeTrue();
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.LocalFrame.SetTransform(Transform3D.Identity);
            positionable.WorldFrame.SetTransform(Transform3D.Identity);

            positionable.WorldFrame.SetTransform(t1);
            Utils.AreApproxTheSame(positionable.LocalFrame.GetTransformCopy(), t1).Should().BeTrue();

            positionable.LocalFrame.SetTransform(Transform3D.Identity);
            positionable.WorldFrame.SetTransform(Transform3D.Identity);

            positionable.LocalFrame.SetTransform(t1);
            Utils.AreApproxTheSame(positionable.WorldFrame.GetTransformCopy(), t1).Should().BeTrue();

            positionable.LocalFrame.SetTransform(Transform3D.Identity);
            positionable.WorldFrame.SetTransform(Transform3D.Identity);

            positionable.WorldFrame.X = 1.0;
            positionable.WorldFrame.X.Should().BeApproximately(1.0, eps);
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.WorldFrame.Y = 2.0;
            positionable.WorldFrame.Y.Should().BeApproximately(2.0, eps);
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.WorldFrame.Z = 3.0;
            positionable.WorldFrame.Z.Should().BeApproximately(3.0, eps);
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.WorldFrame.Rx = 4.0;
            positionable.WorldFrame.Rx.Should().BeApproximately(4.0, eps);
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.WorldFrame.Ry = 5.0;
            positionable.WorldFrame.Ry.Should().BeApproximately(5.0, eps);
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();

            positionable.WorldFrame.Rz = 6.0;
            positionable.WorldFrame.Rz.Should().BeApproximately(6.0, eps);
            monitor.Should()
                .Raise(nameof(IPositionable.PositionModified))
                .WithSender(positionable)
                .WithArgs<PositionModifiedEventArgs>(p => p.MovedObject == positionable);
            monitor.Clear();
        }
    }
}
