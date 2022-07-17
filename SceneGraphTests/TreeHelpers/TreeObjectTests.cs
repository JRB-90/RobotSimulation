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
    public class TreeObjectTests
    {
        [Fact]
        public void Can_Linkage_Pass_Tree_Object_Tests()
        {
            IWindsorContainer container = SceneGraphHelpers.BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;
            ISceneEntity entity1 = scene.Root.CreateNewEntity();
            ILinkageContainer linkages = entity1.LinkageContainer;

            StandardTreeObjectTests(
                () => linkages.BaseLinkage.CreateNewLinkage()
            );

            StandardGenericTreeObjectTests<ILinkage>(
                linkages.BaseLinkage,
                () => linkages.BaseLinkage.CreateNewLinkage()
            );
        }

        private void StandardTreeObjectTests(
            Func<ITreeObject> createTreeObject)
        {
            var treeObject1 = createTreeObject();
            treeObject1.Should().NotBeNull();
            treeObject1.ID.Should().NotBeEmpty();
            treeObject1.Name.Should().NotBeNullOrEmpty();
            treeObject1.Name.Should().NotBeNullOrWhiteSpace();
            
            var treeObject2 = createTreeObject();
            treeObject2.Should().NotBeNull();
            treeObject2.ID.Should().NotBe(treeObject1.ID);
            treeObject2.Name.Should().NotBe(treeObject1.Name);

            var treeObject3 = createTreeObject();
            var monitor = treeObject3.Monitor();
            treeObject3.Name = "TestName";
            monitor.Should()
                .Raise(nameof(ILinkage.ObjectModified))
                .WithSender(treeObject3)
                .WithArgs<TreeObjectModifiedEventArgs>();

            treeObject3.Name = treeObject1.Name;
            treeObject3.Name.Should().Be("TestName");

            treeObject1.ID.Should().NotBe(treeObject2.ID);
            treeObject1.ID.Should().NotBe(treeObject3.ID);
            treeObject2.ID.Should().NotBe(treeObject3.ID);
        }

        private void StandardGenericTreeObjectTests<TParent>(
            TParent parent,
            Func<ITreeObject<TParent>> createTreeObject)
        {
            var treeObject1 = createTreeObject();
            var monitor = treeObject1.Monitor();

            treeObject1.Should().NotBeNull();
            treeObject1.ID.Should().NotBeEmpty();
            treeObject1.Name.Should().NotBeNullOrEmpty();
            treeObject1.Name.Should().NotBeNullOrWhiteSpace();
            treeObject1.Parent.Should().Be(parent);

            treeObject1.Detach().Should().BeTrue();
            monitor.Should()
                .Raise(nameof(ILinkage.ObjectModified))
                .WithSender(treeObject1)
                .WithArgs<TreeObjectModifiedEventArgs>();
            monitor.Clear();

            treeObject1.Detach().Should().BeFalse();
            treeObject1.Parent.Should().BeNull();
            monitor.Should().NotRaise(nameof(ILinkage.ObjectModified));
            monitor.Clear();

            treeObject1.AttachTo(parent).Should().BeTrue();
            monitor.Should()
                .Raise(nameof(ILinkage.ObjectModified))
                .WithSender(treeObject1)
                .WithArgs<TreeObjectModifiedEventArgs>();
            monitor.Clear();

            treeObject1.AttachTo(parent).Should().BeFalse();
            treeObject1.Parent.Should().Be(parent);
            monitor.Should().NotRaise(nameof(ILinkage.ObjectModified));
            monitor.Clear();

            var treeObject2 = createTreeObject();
            treeObject2.Should().NotBeNull();
            treeObject2.ID.Should().NotBe(treeObject1.ID);
            treeObject2.Name.Should().NotBe(treeObject1.Name);

            var treeObject3 = createTreeObject();
            monitor = treeObject3.Monitor();
            treeObject3.Name = "TestName2";
            monitor.Should()
                .Raise(nameof(ILinkage.ObjectModified))
                .WithSender(treeObject3)
                .WithArgs<TreeObjectModifiedEventArgs>();

            treeObject3.Name = treeObject1.Name;
            treeObject3.Name.Should().Be("TestName2");

            monitor.Clear();
            treeObject3.Parent = default(TParent);
            treeObject3.Parent.Should().Be(default(TParent));
            monitor.Should()
                .Raise(nameof(ILinkage.ObjectModified))
                .WithSender(treeObject3)
                .WithArgs<TreeObjectModifiedEventArgs>();
            monitor.Clear();

            treeObject3.Parent = (TParent)treeObject1;
            treeObject3.Parent.Should().Be(treeObject1);
            monitor.Should()
                .Raise(nameof(ILinkage.ObjectModified))
                .WithSender(treeObject3)
                .WithArgs<TreeObjectModifiedEventArgs>();
        }
    }
}
