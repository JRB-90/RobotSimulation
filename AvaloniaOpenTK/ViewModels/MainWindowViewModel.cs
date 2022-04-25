using Avalonia.Media;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Controls;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
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

            var entity = app.SceneManager.CurrentScene.Root.CreateNewEntity("Entity");
            var geometry = entity.GeometryContainer.Root.CreateChildGeometry("Geometry");
            geometry.SetDrawingData(new List<Vertex> { new Vertex(0, new Vector3D(0, 0, 1)) }, new List<uint> { 0 });
            geometry.GeometryType = GeometryType.Points;

            var controlFactory = container.Resolve<IOpenTKControlFactory>();
            GraphicsControl1 = controlFactory.CreateControl();
            GraphicsControl2 = controlFactory.CreateControl();
            GraphicsControl3 = controlFactory.CreateControl();
            GraphicsControl4 = controlFactory.CreateControl();

            //GraphicsControl1 = new OpenTKControl() { Name = "C1", ClearColor = Brushes.PaleTurquoise };
            //GraphicsControl2 = new OpenTKControl() { Name = "C2", ClearColor = Brushes.PaleGoldenrod };
            //GraphicsControl3 = new OpenTKControl() { Name = "C3", ClearColor = Brushes.PaleGreen };
            //GraphicsControl4 = new OpenTKControl() { Name = "C4", ClearColor = Brushes.PaleVioletRed };

            GraphicsControl1.ClearColor = Brushes.PaleTurquoise;

            GraphicsControl1.Camera.PositionInWorld =
                new Transform3D(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

            GraphicsControl1.Camera.CameraProjection =
                new PerspectiveProjection(
                    GraphicsControl1.SurfaceWidth,
                    GraphicsControl1.SurfaceHeight,
                    70.0,
                    0.01,
                    1000.0
                );

            GraphicsControl2.ClearColor = Brushes.PaleGoldenrod;
            GraphicsControl3.ClearColor = Brushes.PaleGreen;
            GraphicsControl4.ClearColor = Brushes.PaleVioletRed;

            app.DisplayManager.AddSurface(GraphicsControl1);
            app.DisplayManager.AddSurface(GraphicsControl2);
            app.DisplayManager.AddSurface(GraphicsControl3);
            app.DisplayManager.AddSurface(GraphicsControl4);
        }

        public OpenTKControl GraphicsControl1 { get; }

        public OpenTKControl GraphicsControl2 { get; }

        public OpenTKControl GraphicsControl3 { get; }

        public OpenTKControl GraphicsControl4 { get; }

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
