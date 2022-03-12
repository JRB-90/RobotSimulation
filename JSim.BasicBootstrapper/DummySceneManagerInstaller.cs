using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.SceneGraph;

namespace JSim.BasicBootstrapper
{
    /// <summary>
    /// Installs a dummy scene manager to a windsor container.
    /// </summary>
    public class DummySceneManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISceneManager>()
                .ImplementedBy<DummySceneManager>()
            );
        }
    }
}
