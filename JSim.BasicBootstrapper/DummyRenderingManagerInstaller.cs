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
                Component.For<IRenderingManager>()
                .ImplementedBy<DummyRenderingManager>()
            );
        }
    }
}
