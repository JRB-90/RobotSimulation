using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using JSim.Logging;

namespace JSim.Av.Design
{
    internal static class DesignData
    {
        static DesignData()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;
            ILogger logger = container.Resolve<ILogger>();

            ISceneEntity entity = scene.Root.CreateNewEntity("Entity");
            Geometry = entity.GeometryContainer.Root.CreateChildGeometry("TestGeometry");

            Material =
                new Material(
                    new Color(1.0f, 0.3f, 0.2f, 0.1f),
                    new Color(1.0f, 0.8f, 0.7f, 0.5f),
                    new Color(1.0f, 0.1f, 0.9f, 0.7f),
                    0.7,
                    ShadingType.Smooth
                );

            Geometry.Material = Material;
        }

        public static IMaterial Material { get; }

        public static IGeometry Geometry { get; }

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
