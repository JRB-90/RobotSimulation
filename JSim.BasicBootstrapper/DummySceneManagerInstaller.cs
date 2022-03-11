using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.SceneGraph;

namespace JSim.BasicBootstrapper
{
    public class DummySceneManagerInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Installs a dummy scene manager to a windsor container.
        /// </summary>
        /// <param name="container">Container to install to.</param>
        /// <param name="store">Configuration setting storage object.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISceneManager>()
                .ImplementedBy<DummySceneManager>()
            );
        }
    }
}
