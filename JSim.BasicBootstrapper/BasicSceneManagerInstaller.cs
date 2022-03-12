using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core.SceneGraph;

namespace JSim.BasicBootstrapper
{
    /// <summary>
    /// Installs a standard scene manager to a windsor container.
    /// </summary>
    public class BasicSceneManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<INameRepository>()
                .ImplementedBy<NameRepository>()
            );

            container.Register(
                Component.For<ISceneAssembly>()
                .ImplementedBy<SceneAssembly>()
            );

            container.Register(
                Component.For<ISceneEntity>()
                .ImplementedBy<SceneEntity>()
            );

            container.Register(
                Component.For<ISceneManager>()
                .ImplementedBy<SceneManager>()
            );
        }
    }
}
