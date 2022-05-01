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

            var cube = CubeBuilder.Build(1.0, 1.0, 1.0);

            var cubePoints = entity1.GeometryContainer.Root.CreateChildGeometry("CubePoints");
            cubePoints.IsVisible = true;
            cubePoints.SetDrawingData(cube.Item1, cube.Item2);
            cubePoints.GeometryType = GeometryType.Points;
            cubePoints.Material.Color = new Color(0.8f, 0.1f, 0.1f);

            var cubeLines = entity1.GeometryContainer.Root.CreateChildGeometry("CubeLines");
            cubeLines.IsVisible = false;
            cubeLines.SetDrawingData(cube.Item1, cube.Item2);
            cubeLines.GeometryType = GeometryType.Wireframe;
            cubeLines.Material.Color = new Color(0.5f, 0.5f, 0.5f);

            var cubeSolid = entity1.GeometryContainer.Root.CreateChildGeometry("CubeSolid");
            cubeSolid.IsVisible = true;
            cubeSolid.SetDrawingData(cube.Item1, cube.Item2);
            cubeSolid.GeometryType = GeometryType.Solid;
            cubeSolid.Material.Color = new Color(0.2f, 0.7f, 0.2f);

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
                control.Camera.PositionInWorld = new Transform3D(3, 3, 3, 0, 0, 0);
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
            entity.LocalFrame = new Transform3D(0.0, 0.0, 0.0, rx, ry, rz);
            
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
