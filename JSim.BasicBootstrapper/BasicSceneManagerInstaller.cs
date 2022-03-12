using Castle.Facilities.TypedFactory;
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
                .Named("SceneAssembly")
                .ImplementedBy<SceneAssembly>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<ISceneAssemblyFactory>().AsFactory()
            );

            container.Register(
                Component.For<ISceneEntity>()
                .Named("SceneEntity")
                .ImplementedBy<SceneEntity>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<ISceneEntityFactory>().AsFactory()
            );

            container.Register(
                Component.For<ISceneObjectCreator>()
                .ImplementedBy<SceneObjectCreator>()
            );

            container.Register(
                Component.For<IScene>()
                .Named("Scene")
                .ImplementedBy<Scene>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<ISceneFactory>().AsFactory()
            );

            container.Register(
                Component.For<ISceneManager>()
                .ImplementedBy<SceneManager>()
            );
        }
    }
}
