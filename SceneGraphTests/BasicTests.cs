using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using FluentAssertions;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.SceneGraph;
using JSim.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SceneGraphTests
{
    public class BasicTests
    {
        const string SceneFileName = "TestScene.jsc";

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

            app.Dispose();
        }

        [Fact]
        public void CanIterateScene()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            PopulateTestScene(app.SceneManager.CurrentScene);
            app.SceneManager.CurrentScene.Count().Should().Be(12);

            app.Dispose();
        }

        [Fact]
        public void CanManipulateScene()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            IScene scene = app.SceneManager.CurrentScene;

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

            app.SceneManager.CurrentScene.Count().Should().Be(12);

            assembly2.MoveAssembly(assembly1).Should().BeTrue();
            entity1.MoveAssembly(assembly5).Should().BeTrue();

            scene.Root.Children.Should().NotContain((ISceneObject)assembly2);
            assembly2.ParentAssembly.Should().BeSameAs(assembly1);
            assembly1.Children.Should().Contain((ISceneObject)assembly2);

            scene.Root.Children.Should().NotContain(entity1);
            entity1.ParentAssembly.Should().BeSameAs(assembly5);
            assembly5.Children.Should().Contain(entity1);

            scene.TryFindByID(assembly1.ID, out ISceneObject? foundAssembly).Should().BeTrue();
            foundAssembly.Should().NotBeNull();
            foundAssembly.Should().BeSameAs(assembly1);

            scene.TryFindByName(entity1.Name, out ISceneObject? foundEntity).Should().BeTrue();
            foundEntity.Should().NotBeNull();
            foundEntity.Should().BeSameAs(entity1);

            app.Dispose();
        }

        [Fact]
        public void CanNewScene()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            PopulateTestScene(app.SceneManager.CurrentScene);
            app.SceneManager.CurrentScene.Count().Should().Be(12);

            app.SceneManager.NewScene();
            app.SceneManager.CurrentScene.Count().Should().Be(0);

            app.Dispose();
        }

        [Fact]
        public void CanSaveLoadScene()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            PopulateTestScene(app.SceneManager.CurrentScene);

            var sceneObjsPre =
                app.SceneManager.CurrentScene
                .Select(o => o.Name);

            string path = Path.GetTempPath() + SceneFileName;
            app.SceneManager.SaveScene(path);
            app.SceneManager.NewScene();
            app.SceneManager.LoadScene(path);

            var sceneObjsPost =
                app.SceneManager.CurrentScene
                .Select(o => o.Name);

            sceneObjsPost.Count().Should().Be(sceneObjsPre.Count());

            foreach (var sceneObj in sceneObjsPre)
            {
                sceneObjsPost.Contains(sceneObj).Should().BeTrue();
            }

            app.Dispose();
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

        private void PopulateTestScene(IScene scene)
        {
            var assembly1 = scene.Root.CreateNewAssembly("Assembly1");
            var assembly2 = scene.Root.CreateNewAssembly("Assembly2");
            var assembly3 = scene.Root.CreateNewAssembly("Assembly3");
            var entity1 = scene.Root.CreateNewEntity("Entity1");
            var entity2 = scene.Root.CreateNewEntity("Entity2");

            var entity3 = assembly2.CreateNewEntity("Entity3");
            var entity4 = assembly2.CreateNewEntity("Entity4");

            var entity5 = assembly3.CreateNewEntity("Entity5");
            var assembly4 = assembly3.CreateNewAssembly("Assembly4");
            var assembly5 = assembly3.CreateNewAssembly("Assembly5");

            var entity6 = assembly4.CreateNewEntity("Entity6");
            var entity7 = assembly4.CreateNewEntity("Entity7");
        }
    }
}