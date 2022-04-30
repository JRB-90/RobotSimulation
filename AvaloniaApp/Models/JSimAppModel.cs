using Avalonia.Controls;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Shared;
using JSim.Avalonia.ViewModels;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Logging;
using JSim.OpenTK;

namespace AvaloniaApp.Models
{
    public class JSimAppModel
    {
        readonly ISimApplication app;

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
            OpenTKControlFactory = container.Resolve<IOpenTKControlFactory>();
            
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
        }

        public MainMenuViewModel MainMenuVM { get; }

        public SceneTreeViewModel SceneTreeVM { get; }

        public SceneObjectViewModel SceneObjectVM { get; }

        public IOpenTKControlFactory OpenTKControlFactory { get; }
    }
}
