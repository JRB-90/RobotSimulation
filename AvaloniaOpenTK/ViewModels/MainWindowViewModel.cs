using Avalonia.Media;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Controls;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.Render.GeometryBuilders;
using JSim.Logging;
using JSim.OpenTK;
using System.Collections.Generic;

namespace AvaloniaOpenTK.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ISimApplication app;

        public MainWindowViewModel()
        {
            IWindsorContainer container = BootstrapContainer();
            app = container.Resolve<ISimApplication>();

            var cube = CubeBuilder.Build(1.0, 1.0, 1.0);
            var entity = app.SceneManager.CurrentScene.Root.CreateNewEntity("Entity");

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

            var controlFactory = container.Resolve<IOpenTKControlFactory>();
            GraphicsControl = controlFactory.CreateControl();

            GraphicsControl.ClearColor = new SolidColorBrush(Avalonia.Media.Color.FromRgb(32, 32, 56));

            GraphicsControl.Camera.PositionInWorld =
                new Transform3D(3, -2, 2, 0, 0, 0);
            GraphicsControl.Camera.LookAtPoint(Vector3D.Origin, Vector3D.UnitZ);

            GraphicsControl.Camera.CameraProjection =
                new PerspectiveProjection(
                    GraphicsControl.SurfaceWidth,
                    GraphicsControl.SurfaceHeight,
                    70.0,
                    0.01,
                    1000.0
                );

            app.DisplayManager.AddSurface(GraphicsControl);
        }

        public OpenTKControl GraphicsControl { get; }

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
    }
}
