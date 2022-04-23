using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Render;
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

            IGeometryCreator creator = container.Resolve<IGeometryCreator>();
            IGeometry geo1 = creator.CreateGeometry(null);
            IGeometry geo2 = geo1.CreateChildGeometry();

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
