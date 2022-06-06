using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.AvGL;
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

            var suzanne =
                app.SceneManager.ModelImporter.LoadModel(
                    @"C:\Development\Test\Suzanne.stl",
                    scene.Root
                );
            var geometry =
                ((ISceneAssembly)suzanne)
                .Children
                .OfType<ISceneAssembly>()
                .First()
                .OfType<ISceneEntity>()
                .First()
                .GeometryContainer.Root.Children
                .First();

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
            GeometryTree = new GeometryTree() { GeometryContainer = entity.GeometryContainer };
            GeometryTree.SelectedGeometry.Subscribe(g => OnGeometrySelectionChanged(g));

            SceneObjectControl = new SceneObjectControl() { };
            SceneTree = new SceneTree() { Scene = scene };
            SceneTree.SelectedObjects.Subscribe(o => OnSceneObjectSelectionChanged(o));

            DisplayedObjectControl = SceneObjectControl;

            OpenGLControl = new OpenGLControl();
        }

        public GeometryControl GeometryControl { get; }
        public SceneObjectControl SceneObjectControl { get; }
        public GeometryTree GeometryTree { get; }
        public SceneTree SceneTree { get; }
        public OpenGLControl OpenGLControl { get;}

        public object? DisplayedObjectControl
        {
            get => displayedObjectControl;
            set => this.RaiseAndSetIfChanged(ref displayedObjectControl, value);
        }

        public int SelectedTab
        {
            get => selectedTab;
            set
            {
                selectedTab = value;

                if (selectedTab == 0)
                {
                    DisplayedObjectControl = SceneObjectControl;
                }
                else
                {
                    DisplayedObjectControl = GeometryControl;
                }
            }
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

        private void OnGeometrySelectionChanged(IReadOnlyCollection<IGeometry>? selectedGeometry)
        {
            if (selectedGeometry != null)
            {
                GeometryControl.Geometry = selectedGeometry.FirstOrDefault();
            }
        }

        private void OnSceneObjectSelectionChanged(IReadOnlyCollection<ISceneObject>? sceneObjects)
        {
            if (sceneObjects != null)
            {
                var sceneObject = sceneObjects.FirstOrDefault();
                SceneObjectControl.SceneObject = sceneObject;

                if (sceneObject != null &&
                    sceneObject is ISceneEntity entity)
                {
                    GeometryTree.GeometryContainer = entity.GeometryContainer;
                    GeometryControl.Geometry = entity.GeometryContainer.Root;
                }
                else
                {
                    GeometryTree.GeometryContainer = null;
                    GeometryControl.Geometry = null;
                }
            }
        }

        private ISceneEntity entity;
        private int selectedTab;
        private object? displayedObjectControl;
    }
}
