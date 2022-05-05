using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.SceneGraph;
using JSim.Logging;
using JSim.OpenTK;

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

            double angle = 45.0;
            Vector3D pos = new Vector3D(3, 0, 0);
            SphericalCoords sph = new SphericalCoords(pos);
            sph.Azimuth += angle.ToRad();
            Vector3D newPos = sph.ToCartesian();

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
