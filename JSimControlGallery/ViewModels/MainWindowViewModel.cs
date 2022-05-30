using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using JSim.Logging;
using JSimControlGallery.Controls;
using System.Linq;

namespace JSimControlGallery.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            IWindsorContainer container = BootstrapContainer();
            ISimApplication app = container.Resolve<ISimApplication>();
            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;

            var suzanne =
                app.SceneManager.ModelImporter.LoadModel(
                    @"C:\Development\Test\Suzanne.stl",
                    scene.Root
                );
            Material =
                ((ISceneAssembly)suzanne)
                .Children
                .OfType<ISceneAssembly>()
                .First()
                .OfType<ISceneEntity>()
                .First()
                .GeometryContainer.Root.Children
                .First()
                .Material;

            //var fanuc =
            //    app.SceneManager.ModelImporter.LoadModel(
            //        @"C:\Development\Test\robot.3ds",
            //        scene.Root
            //    );
            //Material =
            //    ((ISceneAssembly)fanuc)
            //    .Children
            //    .OfType<ISceneAssembly>()
            //    .Skip(1)
            //    .First()
            //    .OfType<ISceneEntity>()
            //    .First()
            //    .GeometryContainer.Root.Children
            //    .First()
            //    .Material;

            MaterialControl = new MaterialControl() {  Material = Material };
        }

        public IMaterial Material { get; }

        public MaterialControl MaterialControl { get; }

        private static IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Install(
                new BasicApplicationInstaller(),
                Log4NetInstaller.FromEmbedded("log4netconsole.config"),
                new BasicSceneManagerInstaller(),
                new DummyRenderingManagerInstaller()
            );

            return container;
        }
    }
}
