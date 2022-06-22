using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.Render;

namespace JSim.AvGL
{
    /// <summary>
    /// Installs an Avalonia OpenGL rendering backend.
    /// </summary>
    public class OpenGLInstaller : IWindsorInstaller
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
                .Named("OpenGLGeometry")
                .ImplementedBy<OpenGLGeometry>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IOpenGLGeometryFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IGeometryCreator>()
                .Named("OpenGLGeometryCreator")
                .ImplementedBy<OpenGLGeometryCreator>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IGeometryCreatorFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IRenderingEngine>()
                .Named("OpenGLRenderingEngine")
                .ImplementedBy<OpenGLRenderingEngine>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<ISharedGlContextFactory>()
                .Named("SharedContextFactory")
                .ImplementedBy<SharedContextFactory>()
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
                .Named("OpenGLControl")
                .ImplementedBy<OpenGLControl>()
                .LifestyleTransient()
            );

            container.Register(
                Component.For<IOpenGLControlFactory>()
                .Named("OpenGLControlFactory")
                .ImplementedBy<OpenGLControlFactory>()
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
