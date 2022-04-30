using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// Installs an OpenTK based rendering backend.
    /// </summary>
    public class OpenTKInstaller : IWindsorInstaller
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
                .Named("OpenTKGeometry")
                .ImplementedBy<OpenTKGeometry>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IOpenTKGeometryFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IGeometryCreator>()
                .Named("OpenTKGeometryCreator")
                .ImplementedBy<OpenTKGeometryCreator>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IGeometryCreatorFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IRenderingEngine>()
                .Named("OpenTKRenderingEngine")
                .ImplementedBy<OpenTKRenderingEngine>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<ISharedGlContextFactory>()
                .Named("OpenTKSharedContextFactory")
                .ImplementedBy<OpenTKSharedContextFactory>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<IGlContextManager>()
                .Named("GlContextManager")
                .ImplementedBy<GlContextManager>()
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
                .Named("OpenTKControl")
                .ImplementedBy<OpenTKControl>()
                .LifestyleTransient()
            );

            container.Register(
                Component.For<IOpenTKControlFactory>()
                .Named("OpenTKControlFactory")
                .ImplementedBy<OpenTKControlFactory>()
                .LifestyleSingleton()
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
