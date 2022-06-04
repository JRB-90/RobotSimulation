using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using JSim.Logging;
using JSimControlGallery.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
            //var geometry =
            //    ((ISceneAssembly)suzanne)
            //    .Children
            //    .OfType<ISceneAssembly>()
            //    .First()
            //    .OfType<ISceneEntity>()
            //    .First()
            //    .GeometryContainer.Root.Children
            //    .First();

            //var fanuc =
            //    app.SceneManager.ModelImporter.LoadModel(
            //        @"C:\Development\Test\robot.3ds",
            //        scene.Root
            //    );
            //var geometry =
            //    ((ISceneAssembly)fanuc)
            //    .Children
            //    .OfType<ISceneAssembly>()
            //    .Skip(1)
            //    .First()
            //    .OfType<ISceneEntity>()
            //    .First()
            //    .GeometryContainer.Root.Children
            //    .First();

            entity = scene.Root.CreateNewEntity("Entity");
            var geo1 = entity.GeometryContainer.Root.CreateChildGeometry("Geo1");
            var geo2 = entity.GeometryContainer.Root.CreateChildGeometry("Geo2");
            var geo3 = geo2.CreateChildGeometry("Geo3");
            var geo4 = geo3.CreateChildGeometry("Geo4");
            var geo5 = geo3.CreateChildGeometry("Geo5");

            GeometryControl = new GeometryControl() { };
            GeometryTree = new GeometryTree() { GeometryContainer = Entity.GeometryContainer };
            GeometryTree.SelectedGeometry.Subscribe(g => OnGeometrySelectionChanged(g));
        }

        public ISceneEntity Entity
        {
            get => entity;
            set
            {
                this.RaiseAndSetIfChanged(ref entity, value);
                GeometryTree.GeometryContainer = entity.GeometryContainer;
            }
        }

        public GeometryControl GeometryControl { get; }

        public GeometryTree GeometryTree { get; }

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

        private void OnGeometrySelectionChanged(IReadOnlyCollection<IGeometry>? selectedGeometry)
        {
            if (selectedGeometry != null)
            {
                GeometryControl.Geometry = selectedGeometry.FirstOrDefault();
            }
        }

        private ISceneEntity entity;
    }
}
