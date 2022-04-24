using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using FluentAssertions;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.SceneGraph;
using JSim.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SceneGraphTests
{
    public class SceneObjectTests
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

            var monitoredScene1 = scene.Monitor();

            var assembly1 = scene.Root.CreateNewAssembly();
            monitoredScene1.Should()
                .Raise(nameof(scene.SceneObjectModified))
                .WithSender(scene)
                .WithArgs<SceneObjectModifiedEventArgs>(e => e.SceneObject == scene.Root);
            monitoredScene1.Should()
                .Raise(nameof(scene.SceneStructureChanged))
                .WithSender(scene)
                .WithArgs<SceneStructureChangedEventArgs>(e => e.RootAssembly == scene.Root);

            var assembly2 = scene.Root.CreateNewAssembly();
            var assembly3 = scene.Root.CreateNewAssembly();
            var entity1 = scene.Root.CreateNewEntity();

            var monitoredScene2 = scene.Monitor();
            var entity2 = scene.Root.CreateNewEntity();
            monitoredScene2.Should()
                .Raise(nameof(scene.SceneObjectModified))
                .WithSender(scene)
                .WithArgs<SceneObjectModifiedEventArgs>(e => e.SceneObject == scene.Root);
            monitoredScene2.Should()
                .Raise(nameof(scene.SceneStructureChanged))
                .WithSender(scene)
                .WithArgs<SceneStructureChangedEventArgs>(e => e.RootAssembly == scene.Root);

            var entity3 = assembly2.CreateNewEntity();
            var entity4 = assembly2.CreateNewEntity();

            var entity5 = assembly3.CreateNewEntity();
            var assembly4 = assembly3.CreateNewAssembly();
            var assembly5 = assembly3.CreateNewAssembly();

            var entity6 = assembly4.CreateNewEntity();

            var monitoredScene3 = scene.Monitor();
            var entity7 = assembly4.CreateNewEntity();
            monitoredScene3.Should()
               .Raise(nameof(scene.SceneObjectModified))
               .WithSender(scene)
               .WithArgs<SceneObjectModifiedEventArgs>(e => e.SceneObject == assembly4);
            monitoredScene3.Should()
                .Raise(nameof(scene.SceneStructureChanged))
                .WithSender(scene)
                .WithArgs<SceneStructureChangedEventArgs>(e => e.RootAssembly == assembly4);

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

            var sceneMonitor = scene.Monitor();
            scene.Name = "SceneTest";
            scene.Name.Should().Be("SceneTest");
            sceneMonitor.Should().Raise(nameof(scene.NameChanged));

            var assembly1 = scene.Root.CreateNewAssembly();
            var assembly2 = scene.Root.CreateNewAssembly();
            var assembly3 = assembly2.CreateNewAssembly();

            var entity1 = scene.Root.CreateNewEntity();
            var entity2 = scene.Root.CreateNewEntity();
            var entity3 = assembly3.CreateNewEntity();
            var entity4 = assembly3.CreateNewEntity();

            var assembly1Monitor = assembly1.Monitor();
            assembly1.Name = "TestAssembly1";
            assembly1.Name.Should().Be("TestAssembly1");
            assembly1Monitor.Should().Raise(nameof(assembly1.SceneObjectModified));

            var entity1Monitor = entity1.Monitor();
            entity1.Name = "TestEntity1";
            entity1.Name.Should().Be("TestEntity1");
            entity1Monitor.Should().Raise(nameof(entity1.SceneObjectModified));

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
        public void DoSceneObjectPositionsUpdate()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            IScene scene = app.SceneManager.CurrentScene;

            var assembly1 = scene.Root.CreateNewAssembly();
            var entity1 = scene.Root.CreateNewEntity();

            CheckTransformsEquivalent(assembly1.LocalFrame, Transform3D.Identity);
            CheckTransformsEquivalent(assembly1.WorldFrame, Transform3D.Identity);
            CheckTransformsEquivalent(entity1.LocalFrame, Transform3D.Identity);
            CheckTransformsEquivalent(entity1.WorldFrame, Transform3D.Identity);

            assembly1.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            CheckTransformsEquivalent(assembly1.LocalFrame, assembly1.WorldFrame);

            var assembly1_1 = assembly1.CreateNewAssembly();
            var entity1_1 = assembly1.CreateNewEntity();

            CheckTransformsEquivalent(assembly1_1.WorldFrame, assembly1.WorldFrame);
            CheckTransformsEquivalent(assembly1_1.LocalFrame, Transform3D.Identity);
            CheckTransformsEquivalent(entity1_1.WorldFrame, assembly1.WorldFrame);
            CheckTransformsEquivalent(entity1_1.LocalFrame, Transform3D.Identity);

            assembly1_1.LocalFrame = new Transform3D(10, 20, 30, 40, 50, 60);
            CheckTransformsEquivalent(assembly1_1.WorldFrame, assembly1.WorldFrame * assembly1_1.LocalFrame);
            entity1_1.LocalFrame = new Transform3D(-4, -5, -6, -1, -2, -3);
            CheckTransformsEquivalent(entity1_1.WorldFrame, assembly1.WorldFrame * entity1_1.LocalFrame);

            assembly1.LocalFrame = new Transform3D(15, 10, 5, -12, 18, -30);
            CheckTransformsEquivalent(assembly1.WorldFrame, assembly1.LocalFrame);
            CheckTransformsEquivalent(assembly1_1.WorldFrame, assembly1.WorldFrame * assembly1_1.LocalFrame);
            CheckTransformsEquivalent(entity1_1.WorldFrame, assembly1.WorldFrame * entity1_1.LocalFrame);

            assembly1.WorldFrame = new Transform3D(12, 13, 14, 15, 16, 17);
            CheckTransformsEquivalent(assembly1.WorldFrame, assembly1.LocalFrame);
            CheckTransformsEquivalent(assembly1_1.WorldFrame, assembly1.WorldFrame * assembly1_1.LocalFrame);
            CheckTransformsEquivalent(entity1_1.WorldFrame, assembly1.WorldFrame * entity1_1.LocalFrame);

            assembly1_1.WorldFrame = new Transform3D(5, -6, 7, -8, 9, -10);
            CheckTransformsEquivalent(assembly1_1.WorldFrame, assembly1.WorldFrame * assembly1_1.LocalFrame);
            entity1_1.WorldFrame = new Transform3D(15, -3, 80, 45, -30, 12);
            CheckTransformsEquivalent(entity1_1.WorldFrame, assembly1.WorldFrame * entity1_1.LocalFrame);

            app.Dispose();
        }

        [Fact]
        public void CanNewScene()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            PopulateTestScene(app.SceneManager.CurrentScene);
            app.SceneManager.CurrentScene.Count().Should().Be(12);

            using var monitoredSceneManager = app.SceneManager.Monitor();
            app.SceneManager.NewScene();

            monitoredSceneManager.Should().Raise("CurrentSceneChanged");
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

            using var monitoredSceneManager = app.SceneManager.Monitor();

            string path = Path.GetTempPath() + SceneFileName;
            app.SceneManager.SaveScene(path);
            app.SceneManager.NewScene();
            monitoredSceneManager.Should().Raise("CurrentSceneChanged");

            app.SceneManager.LoadScene(path);
            monitoredSceneManager.Should().Raise("CurrentSceneChanged");

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
                new DummyRenderingManagerInstaller()
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

        private void CheckTransformsEquivalent(Transform3D t1, Transform3D t2)
        {
            t1.Translation.X.Should().BeApproximately(t2.Translation.X, 0.0001);
            t1.Translation.X.Should().BeApproximately(t2.Translation.X, 0.0001);
            t1.Translation.X.Should().BeApproximately(t2.Translation.X, 0.0001);
            t1.Rotation.AsFixed().Rx.Should().BeApproximately(t2.Rotation.AsFixed().Rx, 0.0001);
            t1.Rotation.AsFixed().Ry.Should().BeApproximately(t2.Rotation.AsFixed().Ry, 0.0001);
            t1.Rotation.AsFixed().Rz.Should().BeApproximately(t2.Rotation.AsFixed().Rz, 0.0001);
        }
    }
}