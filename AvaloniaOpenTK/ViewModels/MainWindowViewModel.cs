using Avalonia.Media;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
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

namespace AvaloniaOpenTK.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ISimApplication app;
        readonly IOpenTKControlFactory controlFactory;

        public MainWindowViewModel()
        {
            IWindsorContainer container = BootstrapContainer();
            app = container.Resolve<ISimApplication>();
            controlFactory = container.Resolve<IOpenTKControlFactory>();

            var cube = CubeBuilder.Build(1.0, 1.0, 1.0);
            entity = app.SceneManager.CurrentScene.Root.CreateNewEntity("Entity");

            var cubePoints = entity.GeometryContainer.Root.CreateChildGeometry("CubePoints");
            cubePoints.IsVisible = true;
            cubePoints.SetDrawingData(cube.Item1, cube.Item2);
            cubePoints.GeometryType = GeometryType.Points;
            cubePoints.Material.Color = new JSim.Core.Render.Color(0.8f, 0.1f, 0.1f);

            var cubeLines = entity.GeometryContainer.Root.CreateChildGeometry("CubeLines");
            cubeLines.IsVisible = false;
            cubeLines.SetDrawingData(cube.Item1, cube.Item2);
            cubeLines.GeometryType = GeometryType.Wireframe;
            cubeLines.Material.Color = new JSim.Core.Render.Color(0.5f, 0.5f, 0.5f);

            var cubeSolid = entity.GeometryContainer.Root.CreateChildGeometry("CubeSolid");
            cubeSolid.IsVisible = true;
            cubeSolid.SetDrawingData(cube.Item1, cube.Item2);
            cubeSolid.GeometryType = GeometryType.Solid;
            cubeSolid.Material.Color = new JSim.Core.Render.Color(0.2f, 0.7f, 0.2f);


            GraphicsControl1 =
                CreateGrpahicsControl(
                    new SolidColorBrush(Avalonia.Media.Color.FromRgb(32, 32, 56)),
                    new Transform3D(3, -2, 2, 0, 0, 0)
                );

            GraphicsControl2 =
                CreateGrpahicsControl(
                    Brushes.AliceBlue,
                    new Transform3D(5, 2, 3, 0, 0, 0)
                );

            GraphicsControl3 =
                CreateGrpahicsControl(
                    Brushes.Beige,
                    new Transform3D(0, -6, 0, 0, 0, 0)
                );

            GraphicsControl4 =
                CreateGrpahicsControl(
                    Brushes.Cyan,
                    new Transform3D(-3, 1, -4, 0, 0, 0)
                );

            app.SceneManager.CurrentSceneChanged += SceneManager_CurrentSceneChanged;

            timer = new Timer(new TimerCallback(Update), null, 100, 10);
        }

        public OpenTKControl? GraphicsControl1 { get; }

        public OpenTKControl? GraphicsControl2 { get; }

        public OpenTKControl? GraphicsControl3 { get; }

        public OpenTKControl? GraphicsControl4 { get; }

        private OpenTKControl CreateGrpahicsControl(
            IBrush clearColor,
            Transform3D camPosition)
        {
            var graphicsControl = controlFactory.CreateControl();

            graphicsControl.ClearColor = clearColor;
            graphicsControl.Camera.PositionInWorld = camPosition;
            graphicsControl.Camera.LookAtPoint(Vector3D.Origin, Vector3D.UnitZ);

            graphicsControl.Camera.CameraProjection =
                new PerspectiveProjection(
                    graphicsControl.SurfaceWidth,
                    graphicsControl.SurfaceHeight,
                    70.0,
                    0.01,
                    1000.0
                );

            graphicsControl.Scene = app.SceneManager.CurrentScene;
            app.DisplayManager.AddSurface(graphicsControl);

            return graphicsControl;
        }

        private void SceneManager_CurrentSceneChanged(object sender, CurrentSceneChangedEventArgs e)
        {
            GraphicsControl1.Scene = app.SceneManager.CurrentScene;
        }

        private void Update(object? state)
        {
            var rx = _stopwatch.Elapsed.TotalSeconds * 20;
            var ry = _stopwatch.Elapsed.TotalSeconds * 30;
            var rz = _stopwatch.Elapsed.TotalSeconds * 40;
            entity.LocalFrame = new Transform3D(0.0, 0.0, 0.0, rx, ry, rz);
            //GraphicsControl1.InvalidateVisual();
        }

        private static IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Install(
                new BasicApplicationInstaller(),
                Log4NetInstaller.FromEmbedded("log4netconsole.config"),
                new BasicSceneManagerInstaller(),
                new OpenTKInstaller()
            );

            return container;
        }

        private ISceneEntity entity;
        private Timer timer;
        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    }
}
