using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Events;
using JSim.Avalonia.Models;
using JSim.Avalonia.Shared;
using JSim.Avalonia.ViewModels;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Logging;

namespace AvaloniaControlsTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ISimApplication app;

        public MainWindowViewModel(
            InputManager inputManager,
            DialogManager dialogManager)
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

            assembly3.WorldFrame = new Transform3D(10, 20, 30, 40, 50, 60);

            assembly4.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            assembly4.LocalFrame = new Transform3D(-7, -8, -9, -10, -11, -12);

            entity5.WorldFrame = new Transform3D(1, 2, 3, 4, 5, 6);
            entity5.LocalFrame = new Transform3D(-7, -8, -9, -10, -11, -12);

            var transform =
                new Transform3D(
                    1.1111, -202.01223, 891234.87632,
                    0.999999, -123.02434, -122.64
                );

            transformModel = new TransformModel(transform);
            transformModel.TransformModified += TransformModel_TransformModified;
            TransformVM = new Transform3DViewModel(transformModel);

            SceneTreeVM =
                new SceneTreeViewModel(
                    app.SceneManager,
                    inputManager,
                    dialogManager
                );

            SceneObjectVM = new SceneObjectViewModel(app.SceneManager);

            app.SceneManager.CurrentScene.SelectionManager.SetSingleSelection(assembly4);
        }

        public Transform3DViewModel TransformVM { get; }

        public SceneTreeViewModel SceneTreeVM { get; }

        public SceneObjectViewModel SceneObjectVM { get; }

        private void TransformModel_TransformModified(object sender, TransformModifiedEventArgs e)
        {
            if (e.Transform.Translation.X == 55.0)
            {
                transformModel.Transform =
                    new Transform3D(
                        1.1111, -202.01223, 891234.87632,
                        0.999999, -123.02434, -122.64
                    );
            }
        }

        private TransformModel transformModel;
    }
}
