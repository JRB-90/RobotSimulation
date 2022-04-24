using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.Display;

namespace JSim.BasicBootstrapper
{
    /// <summary>
    /// Installs a dummy display manager to a windsor container.
    /// </summary>
    public class DummyDisplayManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRenderingSurface>()
                .Named("DummyRenderingSurface")
                .ImplementedBy<DummyRenderingSurface>()
                .LifestyleTransient()
            );

            container.Register(
                Component.For<IDisplayManager>()
                .Named("DisplayManager")
                .ImplementedBy<DisplayManager>()
            );
        }
    }
}
