using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using FluentAssertions;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.SceneGraph;
using JSim.Logging;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SceneGraphTests
{
    public class BasicTests
    {
        [Fact]
        public void CanResolveContainer()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            app.SceneManager.Should().NotBeNull();
            app.SceneManager.CurrentScene.Should().NotBeNull();

            app.Dispose();
        }

        [Fact]
        public void CanCreateNewSceneObjects()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;

            var assembly1 = scene.Root.CreateNewAssembly();
            var assembly2 = scene.Root.CreateNewAssembly();
            var assembly3 = scene.Root.CreateNewAssembly();
            var entity1 = scene.Root.CreateNewEntity();
            var entity2 = scene.Root.CreateNewEntity();

            var entity3 = assembly2.CreateNewEntity();
            var entity4 = assembly2.CreateNewEntity();

            var entity5 = assembly3.CreateNewEntity();
            var assembly4 = assembly3.CreateNewAssembly();
            var assembly5 = assembly3.CreateNewAssembly();

            var entity6 = assembly4.CreateNewEntity();
            var entity7 = assembly4.CreateNewEntity();

            CheckSceneAssemblyIsValid(assembly1);
            CheckSceneAssemblyIsValid(assembly2);
            CheckSceneAssemblyIsValid(assembly3);
            CheckSceneAssemblyIsValid(assembly4);
            CheckSceneAssemblyIsValid(assembly5);

            CheckSceneEntityIsValid(entity1);
            CheckSceneEntityIsValid(entity2);
            CheckSceneEntityIsValid(entity3);
            CheckSceneEntityIsValid(entity4);
            CheckSceneEntityIsValid(entity5);
            CheckSceneEntityIsValid(entity6);
            CheckSceneEntityIsValid(entity7);

            var sceneObjectList = new List<ISceneObject>();
            foreach (var sceneObject in scene)
            {
                sceneObjectList.Add(sceneObject);
            }

            sceneObjectList.Count.Should().Be(12);
            sceneObjectList.Where(o => o is ISceneAssembly).Count().Should().Be(5);
            sceneObjectList.Where(o => o is ISceneEntity).Count().Should().Be(7);

            assembly1.ParentAssembly.Should().BeSameAs(scene.Root);
            assembly2.ParentAssembly.Should().BeSameAs(scene.Root);
            assembly3.ParentAssembly.Should().BeSameAs(scene.Root);
            entity1.ParentAssembly.Should().BeSameAs(scene.Root);
            entity2.ParentAssembly.Should().BeSameAs(scene.Root);

            entity3.ParentAssembly.Should().BeSameAs(assembly2);
            entity4.ParentAssembly.Should().BeSameAs(assembly2);

            entity5.ParentAssembly.Should().BeSameAs(assembly3);
            assembly4.ParentAssembly.Should().BeSameAs(assembly3);
            assembly5.ParentAssembly.Should().BeSameAs(assembly3);

            entity6.ParentAssembly.Should().BeSameAs(assembly4);
            entity7.ParentAssembly.Should().BeSameAs(assembly4);

            app.Dispose();
        }

        [Fact]
        public void CanRenameObjects()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;

            scene.Name = "SceneTest";
            scene.Name.Should().Be("SceneTest");

            var assembly1 = scene.Root.CreateNewAssembly();
            var assembly2 = scene.Root.CreateNewAssembly();
            var assembly3 = assembly2.CreateNewAssembly();

            var entity1 = scene.Root.CreateNewEntity();
            var entity2 = scene.Root.CreateNewEntity();
            var entity3 = assembly3.CreateNewEntity();
            var entity4 = assembly3.CreateNewEntity();

            assembly1.Name = "TestAssembly1";
            assembly1.Name.Should().Be("TestAssembly1");

            entity1.Name = "TestEntity1";
            entity1.Name.Should().Be("TestEntity1");

            string assembly2OriginalName = assembly2.Name;
            assembly2.Name = assembly1.Name;
            assembly2.Name.Should().NotBe(assembly1.Name);
            assembly2.Name.Should().Be(assembly2OriginalName);

            string entity2OriginalName = entity2.Name;
            entity2.Name = entity1.Name;
            entity2.Name.Should().NotBe(entity1.Name);
            entity2.Name.Should().Be(entity2OriginalName);
        }

        private void CheckSceneAssemblyIsValid(ISceneAssembly assembly)
        {
            assembly.Should().NotBeNull();
            assembly.Name.Should().NotBeNullOrEmpty();
            assembly.ParentAssembly.Should().NotBeNull();
            assembly.Children.Should().NotBeNull();
        }

        private void CheckSceneEntityIsValid(ISceneEntity entity)
        {
            entity.Should().NotBeNull();
            entity.Name.Should().NotBeNullOrEmpty();
            entity.ParentAssembly.Should().NotBeNull();
        }

        private IWindsorContainer BootstrapContainer()
        {
            WindsorContainer container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Install(
                new BasicApplicationInstaller(),
                Log4NetInstaller.FromEmbedded("log4netconsole.config"),
                new BasicSceneManagerInstaller(),
                new DummyRenderingManagerInstaller(),
                new DummyDisplayManagerInstaller()
            );

            return container;
        }
    }
}