using Avalonia.Media;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Controls;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Logging;
using JSim.OpenTK;

namespace AvaloniaOpenTK.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ISimApplication app;

        public MainWindowViewModel()
        {
            IWindsorContainer container = BootstrapContainer();
            app = container.Resolve<ISimApplication>();

            GraphicsControl1 = new OpenTKControl() { Name = "C1", ClearColor = Brushes.PaleTurquoise };
            GraphicsControl2 = new OpenTKControl() { Name = "C2", ClearColor = Brushes.PaleGoldenrod };
            GraphicsControl3 = new OpenTKControl() { Name = "C3", ClearColor = Brushes.PaleGreen };
            GraphicsControl4 = new OpenTKControl() { Name = "C4", ClearColor = Brushes.PaleVioletRed };

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
