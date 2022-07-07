using Castle.Windsor;
using FluentAssertions;
using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Linkages;
using JSim.Core.SceneGraph;
using System;
using Xunit;

namespace SceneGraphTests.TreeHelpers
{
    public class HierarchicalTreeObjectTests
    {
        [Fact]
        public void Can_Linkage_Pass_HierarchicalTreeObject_Tests()
        {
            IWindsorContainer container = SceneGraphHelpers.BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;
            ISceneEntity entity1 = scene.Root.CreateNewEntity();
            ILinkageContainer linkages = entity1.LinkageContainer;

            StandardHierarchicalTreeObjectTests<ILinkage, ILinkage>(
                linkages.BaseLinkage,
                () => linkages.BaseLinkage.CreateNewLinkage()
            );
        }

        private void StandardHierarchicalTreeObjectTests<TParent, TChild> (
            IHierarchicalTreeObject<TParent, TChild> hierarhcicalTreeObjectRoot,
            Func<IHierarchicalTreeObject<TParent, TChild>> createHierarhcicalTreeObject)
            where TParent : ITreeObject
            where TChild : ITreeObject
        {
            hierarhcicalTreeObjectRoot.IsTreeRoot.Should().BeTrue();
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(0);

            var hierarhcical1 = createHierarhcicalTreeObject();
            hierarhcical1.IsTreeRoot.Should().BeFalse();
            hierarhcical1.Parent.Should().Be(hierarhcicalTreeObjectRoot);
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(1);
            hierarhcicalTreeObjectRoot.Children.Should().Contain((TChild)hierarhcical1);

            hierarhcical1.Detach().Should().BeTrue();
            hierarhcical1.IsTreeRoot.Should().BeTrue();
            hierarhcical1.Parent.Should().Be(null);
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(0);

            hierarhcicalTreeObjectRoot.AttachChild((TChild)hierarhcical1).Should().BeTrue();
            hierarhcical1.IsTreeRoot.Should().BeFalse();
            hierarhcical1.Parent.Should().Be(hierarhcicalTreeObjectRoot);
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(1);
            hierarhcicalTreeObjectRoot.Children.Should().Contain((TChild)hierarhcical1);

            hierarhcicalTreeObjectRoot.AttachChild((TChild)hierarhcical1).Should().BeFalse();
            hierarhcical1.IsTreeRoot.Should().BeFalse();
            hierarhcical1.Parent.Should().Be(hierarhcicalTreeObjectRoot);
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(1);
            hierarhcicalTreeObjectRoot.Children.Should().Contain((TChild)hierarhcical1);

            hierarhcicalTreeObjectRoot.DetachChild((TChild)hierarhcical1).Should().BeTrue();
            hierarhcical1.IsTreeRoot.Should().BeTrue();
            hierarhcical1.Parent.Should().Be(null);
            hierarhcical1.IsTreeRoot.Should().BeTrue();
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(0);

            hierarhcicalTreeObjectRoot.DetachChild((TChild)hierarhcical1).Should().BeFalse();
            hierarhcical1.IsTreeRoot.Should().BeTrue();
            hierarhcical1.Parent.Should().Be(null);
            hierarhcical1.IsTreeRoot.Should().BeTrue();
            hierarhcicalTreeObjectRoot.Children.Count.Should().Be(0);
        }
    }
}
