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
        readonly ISimApplication app;

        public MainWindowViewModel()
        {
            IWindsorContainer container = BootstrapContainer();
            app = container.Resolve<ISimApplication>();
            ISceneManager sceneManager = app.SceneManager;
            IScene scene = sceneManager.CurrentScene;

            //var suzanne =
            //    app.SceneManager.ModelImporter.LoadModel(
            //        @"C:\Development\Test\Suzanne.stl",
            //        scene.Root
            //    );
            //geometry =
            //    ((ISceneAssembly)suzanne)
            //    .Children
            //    .OfType<ISceneAssembly>()
            //    .First()
            //    .OfType<ISceneEntity>()
            //    .First()
            //    .GeometryContainer.Root.Children
            //    .First();

            var fanuc =
                app.SceneManager.ModelImporter.LoadModel(
                    @"C:\Development\Test\robot.3ds",
                    scene.Root
                );
            geometry =
                ((ISceneAssembly)fanuc)
                .Children
                .OfType<ISceneAssembly>()
                .Skip(1)
                .First()
                .OfType<ISceneEntity>()
                .First()
                .GeometryContainer.Root.Children
                .First();

            material = geometry.Material;
            material.MaterialModified += Material_MaterialModified;
            geometry.GeometryModified += Geometry_GeometryModified;

            MaterialControl = new MaterialControl() { Material = Material };
            GeometryControl = new GeometryControl() { Geometry = Geometry };
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

        public IGeometry Geometry
        {
            get => geometry;
            set
            {
                this.RaiseAndSetIfChanged(ref geometry, value);
                geometry.GeometryModified += Geometry_GeometryModified;
                GeometryControl.Geometry = Geometry;
            }
        }

        public MaterialControl MaterialControl { get; }

        public GeometryControl GeometryControl { get; }

        public void Check()
        {
            var suzanne =
                app.SceneManager.ModelImporter.LoadModel(
                    @"C:\Development\Test\Suzanne.stl",
                    app.SceneManager.CurrentScene.Root
                );
            Geometry =
                ((ISceneAssembly)suzanne)
                .Children
                .OfType<ISceneAssembly>()
                .First()
                .OfType<ISceneEntity>()
                .First()
                .GeometryContainer.Root.Children
                .First();
            Material = Geometry.Material;
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

        private void Geometry_GeometryModified(object sender, GeometryModifiedEventArgs e)
        {
            Trace.WriteLine("Geometry modified");
        }

        private IMaterial material;
        private IGeometry geometry;
    }
}
