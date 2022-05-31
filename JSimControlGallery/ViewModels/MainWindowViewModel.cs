using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using JSim.Logging;
using JSimControlGallery.Controls;
using ReactiveUI;
using System.Diagnostics;
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
            material =
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

            material.MaterialModified += Material_MaterialModified;
            MaterialControl = new MaterialControl() { Material = Material};
        }

        public IMaterial Material
        {
            get => material;
            set
            {
                this.RaiseAndSetIfChanged(ref material, value);
                material.MaterialModified += Material_MaterialModified;
                MaterialControl.Material = Material;
            }
        }

        public MaterialControl MaterialControl { get; }

        public void Check()
        {
            Material = new Material();
        }

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

        private void Material_MaterialModified(object sender, MaterialModifiedEventArgs e)
        {
            Trace.WriteLine("Material modified");
        }

        private IMaterial material;
    }
}
