using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.Render;

namespace JSim.BasicBootstrapper
{
    public class DummyRenderingManagerInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Installs a dummy rendering manager to a windsor container.
        /// </summary>
        /// <param name="container">Container to install to.</param>
        /// <param name="store">Configuration setting storage object.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRenderingManager>()
                .ImplementedBy<DummyRenderingManager>()
            );
        }
    }
}
