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
        }

        private void StandardGenericTreeObjectTests<TParent>(
            TParent parent,
            Func<ITreeObject<TParent>> createTreeObject)
        {
            var treeObject1 = createTreeObject();
            treeObject1.Should().NotBeNull();
            treeObject1.ID.Should().NotBeEmpty();
            treeObject1.Name.Should().NotBeNullOrEmpty();
            treeObject1.Name.Should().NotBeNullOrWhiteSpace();
            treeObject1.Parent.Should().Be(parent);

            treeObject1.Detach().Should().BeTrue();
            treeObject1.Detach().Should().BeFalse();
            treeObject1.Parent.Should().BeNull();
            treeObject1.AttachTo(parent).Should().BeTrue();
            treeObject1.AttachTo(parent).Should().BeFalse();
            treeObject1.Parent.Should().Be(parent);

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
        }
    }
}
