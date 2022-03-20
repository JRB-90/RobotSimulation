using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.SceneGraph;
using JSim.Logging;
using System;

namespace JSim.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();

            ISceneManager sceneManager = app.SceneManager;
            sceneManager.CurrentSceneChanged += SceneManager_CurrentSceneChanged;

            IScene scene = sceneManager.CurrentScene;
            scene.SceneObjectModified += Scene_SceneObjectModified;
            scene.SceneStructureChanged += Scene_SceneStructureChanged;

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

            var canFindA1 = scene.TryFindByID(assembly1.ID, out ISceneObject? foundAssembly);
            var canFindE1 = scene.TryFindByName(entity1.Name, out ISceneObject? foundEntity);

            sceneManager.NewScene();

            app.Dispose();

            Console.Read();
        }

        private static void SceneManager_CurrentSceneChanged(object sender, CurrentSceneChangedEventArgs e)
        {
        }

        private static void Scene_SceneStructureChanged(object sender, SceneStructureChangedEventArgs e)
        {
        }

        private static void Scene_SceneObjectModified(object sender, SceneObjectModifiedEventArgs e)
        {
        }

        private static IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();
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
