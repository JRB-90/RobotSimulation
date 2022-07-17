using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Linkages;
using JSim.Core.Maths;
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
            IScene scene = sceneManager.CurrentScene;

            ISceneEntity entity1 = scene.Root.CreateNewEntity();
            ILinkageContainer linkages = entity1.LinkageContainer;

            var t1 = new Transform3D(1, 2, 3, 4, 5, 6);
            var t2 = new Transform3D(-10, -20, -30, -1, -2, -3);

            linkages.BaseLinkage.WorldFrame.SetTransform(t1);
            linkages.BaseLinkage.LocalFrame.SetTransform(t2);

            app.Dispose();

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
