using Avalonia.Controls;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Models;
using JSim.Avalonia.Shared;
using JSim.Avalonia.ViewModels;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Logging;

namespace JSim.Avalonia.Design
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
                new DummyRenderingManagerInstaller(),
                new DummyDisplayManagerInstaller()
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

            assembly2.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            assembly2.LocalFrame = new Transform3D(-7, -8, -9, -10, -11, -12);

            entity1.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            entity1.LocalFrame = new Transform3D(-7, -8, -9, -10, -11, -12);

            var window = new Window();
            var inputManager = new InputManager(window);
            var dialogManager = new DialogManager(window);

            app.SceneManager.CurrentScene.SelectionManager.SetSingleSelection(assembly2);

            Transform =
                new Transform3D(
                    1.1111, -202.01223, 891234.87632,
                    0.999999, -123.02434, -122.64
                );

            TransformModel = new TransformModel(Transform);
            Transform3DVM = new Transform3DViewModel(TransformModel);

            SceneTreeVM =
                new SceneTreeViewModel(
                    app.SceneManager,
                    inputManager,
                    dialogManager
                );
        }

        public static Transform3D Transform { get; }

        public static TransformModel TransformModel { get; }

        public static Transform3DViewModel Transform3DVM { get; }

        public static SceneTreeViewModel SceneTreeVM { get; }

        private static ISimApplication app;
    }
}
