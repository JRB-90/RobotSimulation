using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.SceneGraph;
using JSim.Logging;

namespace JSim.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //IWindsorContainer container = BootstrapContainer();
            //ISimApplication app = container.Resolve<ISimApplication>();
            //ISceneManager sceneManager = app.SceneManager;
            //IScene scene = sceneManager.CurrentScene;

            //ISceneEntity entity1 = scene.Root.CreateNewEntity();
            //ISceneEntity entity2 = scene.Root.CreateNewEntity();
            //ISceneEntity entity3 = scene.Root.CreateNewEntity();

            //app.Dispose();


            var q = Quaternion.RotationAboutAxis(Vector3D.UnitX, 10.0);

            Console.Read();
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
