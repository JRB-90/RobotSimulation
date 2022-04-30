using Avalonia.Controls;
using AvaloniaApp.Models;
using AvaloniaApp.ViewModels;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Shared;
using JSim.Avalonia.ViewModels;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Logging;

namespace AvaloniaApp.Design
{
    internal static class DesignData
    {
        static DesignData()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();

            container.Install(
                new BasicApplicationInstaller(),
                Log4NetInstaller.FromEmbedded("log4netconsole.config"),
                new BasicSceneManagerInstaller(),
                new DummyRenderingManagerInstaller()
            );

            app = container.Resolve<ISimApplication>();
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

            var window = new Window();
            var inputManager = new InputManager(window);
            var dialogManager = new DialogManager(window);

            var appModel = new JSimAppModel(window);
            var factory = new DockFactory(appModel);
            var layout = factory.CreateLayout();
            factory.InitLayout(layout);

            MainMenuVM =
                new MainMenuViewModel(
                    app,
                    dialogManager
                );

            MainWindowVM =
                new MainWindowViewModel()
                {
                    Factory = factory,
                    Layout = layout
                };

            SceneTreeVM =
                new SceneTreeViewModel(
                    app.SceneManager,
                    inputManager,
                    dialogManager
                );

            ToolSceneTreeVM =
                new ToolSceneTreeViewModel(
                    "SceneTreeView",
                    "Scene Tree",
                    SceneTreeVM
                );

            SceneObjectVM =
                new SceneObjectViewModel(
                    app.SceneManager
                );

            ToolSceneObjectVM =
                new ToolSceneObjectDataViewModel(
                    "SceneObjectView",
                    "Scene Object Data",
                    SceneObjectVM
                );
                

            app.SceneManager.CurrentScene.SelectionManager.SetSingleSelection(assembly4);
        }

        public static MainMenuViewModel MainMenuVM { get; }

        public static MainWindowViewModel MainWindowVM { get; }

        public static SceneTreeViewModel SceneTreeVM { get; }

        public static ToolSceneTreeViewModel ToolSceneTreeVM { get; }

        public static SceneObjectViewModel SceneObjectVM { get; }

        public static ToolSceneObjectDataViewModel ToolSceneObjectVM { get; }

        private static ISimApplication app;
    }
}
