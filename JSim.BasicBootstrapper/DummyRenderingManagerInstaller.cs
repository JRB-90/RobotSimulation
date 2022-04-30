using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.Render;

namespace JSim.BasicBootstrapper
{
    /// <summary>
    /// Installs a dummy rendering manager to a windsor container.
    /// </summary>
    public class DummyRenderingManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IGeometryContainer>()
                .Named("GeometryContainer")
                .ImplementedBy<GeometryContainer>()
                .LifestyleTransient()
             );

            container.Register(
                Component.For<IGeometry>()
                .Named("DummyGeometry")
                .ImplementedBy<DummyGeometry>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IGeometryFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IGeometryCreator>()
                .Named("GeometryCreator")
                .ImplementedBy<GeometryCreator>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IGeometryCreatorFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IRenderingEngine>()
                .Named("DummyRenderingEngine")
                .ImplementedBy<DummyRenderingEngine>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<IRenderingManager>()
                .Named("RenderingManager")
                .ImplementedBy<RenderingManager>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<IRenderingSurface>()
                .Named("DummyRenderingSurface")
                .ImplementedBy<DummyRenderingSurface>()
                .LifestyleTransient()
            );

            container.Register(
                Component.For<ISurfaceManager>()
                .Named("DisplayManager")
                .ImplementedBy<SurfaceManager>()
                .LifestyleSingleton()
            );
        }
    }
}
