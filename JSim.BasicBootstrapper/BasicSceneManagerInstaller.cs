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
                .Named("NameRepository")
                .ImplementedBy<NameRepository>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<INameRepositoryFactory>().AsFactory()
            );

            container.Register(
                Component.For<ISelectionManager>()
                .Named("SelectionManager")
                .ImplementedBy<SelectionManager>()
                .LifestyleTransient()
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
                .Named("SceneObjectCreator")
                .ImplementedBy<SceneObjectCreator>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<ISceneObjectCreatorFactory>().AsFactory()
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
                Component.For<ISceneIOHandler>()
                .Named("SceneIOHandler")
                .ImplementedBy<XmlSceneIOHandler>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<ISceneManager>()
                .Named("SceneManager")
                .ImplementedBy<SceneManager>()
                .LifestyleSingleton()
            );
        }
    }
}
