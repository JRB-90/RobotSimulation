using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.Display;

namespace JSim.BasicBootstrapper
{
    public class DummyDisplayManagerInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Installs a dummy display manager to a windsor container.
        /// </summary>
        /// <param name="container">Container to install to.</param>
        /// <param name="store">Configuration setting storage object.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDisplayManager>()
                .ImplementedBy<DummyDisplayManager>()
            );
        }
    }
}
