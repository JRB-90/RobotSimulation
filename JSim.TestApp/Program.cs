using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.SceneGraph;
using JSim.Logging;

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

            ISceneEntity entity1 = scene.Root.CreateNewEntity();
            ISceneEntity entity2 = scene.Root.CreateNewEntity();
            ISceneEntity entity3 = scene.Root.CreateNewEntity();

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
                new DummyRenderingManagerInstaller()
            );

            return container;
        }
    }
}
