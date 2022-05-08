using Avalonia.Controls;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Shared;
using JSim.Avalonia.ViewModels;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.Render.GeometryBuilders;
using JSim.Core.SceneGraph;
using JSim.Logging;
using JSim.OpenTK;
using System.Diagnostics;
using System.Threading;

namespace AvaloniaApp.Models
{
    public class JSimAppModel
    {
        readonly ISimApplication app;
        readonly IOpenTKControlFactory openTKControlFactory;

        public JSimAppModel(Window window)
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();

            container.Install(
                new BasicApplicationInstaller(),
                Log4NetInstaller.FromEmbedded("log4netconsole.config"),
                new BasicSceneManagerInstaller(),
                new OpenTKInstaller()
            );

            app = container.Resolve<ISimApplication>();
            openTKControlFactory = container.Resolve<IOpenTKControlFactory>();
            
            var scene = app.SceneManager.CurrentScene;

            var assembly1 = scene.Root.CreateNewAssembly("Assembly1");
            var assembly2 = scene.Root.CreateNewAssembly("Assembly2");
            var assembly3 = scene.Root.CreateNewAssembly("Assembly3");
            var entity1 = scene.Root.CreateNewEntity("Entity1");
            var entity2 = scene.Root.CreateNewEntity("Entity2");

            var entity3 = assembly2.CreateNewEntity("Entity3");
            var entity4 = assembly2.CreateNewEntity("Entity4");

            var entity5 = assembly3.CreateNewEntity("Entity5");
            var assembly4 = assembly3.CreateNewAssembly("Assembly4");
            var assembly5 = assembly3.CreateNewAssembly("Assembly5");

            var entity6 = assembly4.CreateNewEntity("Entity6");
            var entity7 = assembly4.CreateNewEntity("Entity7");

            assembly3.WorldFrame = new Transform3D(10, 20, 30, 40, 50, 60);

            assembly4.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            assembly4.LocalFrame = new Transform3D(-7, -8, -9, -10, -11, -12);

            entity5.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            entity5.LocalFrame = new Transform3D(-7, -8, -9, -10, -11, -12);

            var cube = CubeBuilder.Build(0.2, 0.2, 0.2);

            var cubeSolid = entity1.GeometryContainer.Root.CreateChildGeometry("Cube");
            cubeSolid.LocalFrame = new Transform3D(0.5, 0.5, 0.1, 0.0, 0.0, 10.0);
            cubeSolid.IsVisible = true;
            cubeSolid.SetDrawingData(cube.Item1, cube.Item2);
            cubeSolid.GeometryType = GeometryType.Solid;
            cubeSolid.Material.Color = new Color(1.0f, 0.0f, 0.0f);
            cubeSolid.Material.Shading = ShadingType.Flat;

            double size = 1.0;
            var lineVerts =
                new Vertex[]
                {
                    new Vertex(0, new Vector3D(0, 0, 0)),
                    new Vertex(1, new Vector3D(size, 0, 0)),
                    new Vertex(2, new Vector3D(0, 0, 0)),
                    new Vertex(3, new Vector3D(0, size, 0)),
                    new Vertex(4, new Vector3D(0, 0, 0)),
                    new Vertex(5, new Vector3D(0, 0, size))
                };

            var XIndices = new uint[] { 0, 1 };
            var YIndices = new uint[] { 2, 3 };
            var ZIndices = new uint[] { 4, 5 };

            var xAxis = entity1.GeometryContainer.Root.CreateChildGeometry("XAxis");
            xAxis.SetDrawingData(lineVerts, XIndices);
            xAxis.GeometryType = GeometryType.Wireframe;
            xAxis.Material.Color = new Color(1.0f, 0.0f, 0.0f);

            var yAxis = entity1.GeometryContainer.Root.CreateChildGeometry("YAxis");
            yAxis.SetDrawingData(lineVerts, YIndices);
            yAxis.GeometryType = GeometryType.Wireframe;
            yAxis.Material.Color = new Color(0.0f, 1.0f, 0.0f);

            var zAxis = entity1.GeometryContainer.Root.CreateChildGeometry("ZAxis");
            zAxis.SetDrawingData(lineVerts, ZIndices);
            zAxis.GeometryType = GeometryType.Wireframe;
            zAxis.Material.Color = new Color(0.0f, 0.0f, 1.0f);

            var inputManager = new InputManager(window);
            var dialogManager = new DialogManager(window);

            MainMenuVM =
                new MainMenuViewModel(
                    app,
                    dialogManager
                );

            SceneTreeVM =
                new SceneTreeViewModel(
                    app.SceneManager,
                    inputManager,
                    dialogManager
                );

            SceneObjectVM =
                new SceneObjectViewModel(
                    app.SceneManager
                );

            app.SceneManager.CurrentScene.SelectionManager.SetSingleSelection(assembly4);
            app.SceneManager.CurrentSceneChanged += OnCurrentSceneChanged;

            entity = entity1;
            timer = new Timer(new TimerCallback(Update), null, 100, 10);
        }

        public MainMenuViewModel MainMenuVM { get; }

        public SceneTreeViewModel SceneTreeVM { get; }

        public SceneObjectViewModel SceneObjectVM { get; }

        public Control Create3DControl()
        {
            OpenTKControl control = openTKControlFactory.CreateControl();
            control.Scene = app.SceneManager.CurrentScene;
            
            if (control.Camera != null)
            {
                control.Camera.PositionInWorld = new Transform3D(5, 5, 5, 0, 0, 0);
                control.Camera.LookAtPoint(Vector3D.Origin, Vector3D.UnitZ);
            }
            
            app.SurfaceManager.AddSurface(control);

            return control;
        }

        private void OnCurrentSceneChanged(object sender, CurrentSceneChangedEventArgs e)
        {
            foreach (var surface in app.SurfaceManager.Surfaces)
            {
                surface.Scene = app.SceneManager.CurrentScene;
            }
        }

        private void Update(object? state)
        {
            var rx = _stopwatch.Elapsed.TotalSeconds * 20;
            var ry = _stopwatch.Elapsed.TotalSeconds * 30;
            var rz = _stopwatch.Elapsed.TotalSeconds * 40;
            //entity.LocalFrame = new Transform3D(0.0, 0.0, 0.0, rx, ry, rz);
            
            // TODO - Make render call happen on entity modified

            foreach (var surface in app.SurfaceManager.Surfaces)
            {
                surface.RequestRender();
            }
        }

        private ISceneEntity entity;
        private Timer timer;
        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    }
}
