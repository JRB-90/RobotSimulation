using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core;
using JSim.Core.Common;

namespace JSim.BasicBootstrapper
{
    /// <summary>
    /// Castle windsor installer class for installing a basic JSimApplication.
    /// </summary>
    public class BasicApplicationInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Installs components to a windsor container.
        /// </summary>
        /// <param name="container">Container to install to.</param>
        /// <param name="store">Configuration setting storage object.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMessageCollator>()
                .ImplementedBy<MessageCollator>()
            );

            container.Register(
                Component.For<INameRepository>()
                .Named("NameRepository")
                .ImplementedBy<NameRepository>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<INameRepositoryFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<ISimApplication>()
                .ImplementedBy<SimApplication>()
            );
        }
    }
}
