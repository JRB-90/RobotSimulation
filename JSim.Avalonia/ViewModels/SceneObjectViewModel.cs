using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.ViewModels
{
    public class SceneObjectViewModel : ViewModelBase
    {
        readonly ISceneManager sceneManager;

        public SceneObjectViewModel(ISceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
            ProcessSelection();
            sceneManager.CurrentSceneChanged += OnCurrentSceneChanged;
            sceneManager.CurrentScene.SelectionManager.SelectionChanged += OnSelectionChanged;
        }

        internal SceneObjectDataViewModel? SceneObjectBaseDataVM
        {
            get => sceneObjectBaseDataVM;
            set => this.RaiseAndSetIfChanged(ref sceneObjectBaseDataVM, value, nameof(SceneObjectBaseDataVM));
        }

        internal ISceneObjectTypeDataVM? SceneObjectTypeDataVM
        {
            get => sceneObjectTypeDataVM;
            set => this.RaiseAndSetIfChanged(ref sceneObjectTypeDataVM, value, nameof(SceneObjectTypeDataVM));
        }

        private void ProcessSelection()
        {
            switch (sceneManager.CurrentScene.SelectionManager.SelectionState)
            {
                case SelectionState.SingleSelection:
                    if (sceneManager.CurrentScene.SelectionManager.SelectedObject != null)
                    {
                        SceneObjectBaseDataVM =
                            new SceneObjectDataViewModel(
                                sceneManager.CurrentScene.SelectionManager.SelectedObject
                            );
                        SceneObjectTypeDataVM =
                            SelectObjectTypeVM(
                                sceneManager.CurrentScene.SelectionManager.SelectedObject
                            );
                    }
                    else
                    {
                        sceneManager.CurrentScene.SelectionManager.ResetSelection();
                    }
                    break;
                default:
                    SceneObjectBaseDataVM = null;
                    SceneObjectTypeDataVM = null;
                    break;
            }
        }

        private ISceneObjectTypeDataVM? SelectObjectTypeVM(ISceneObject sceneObject)
        {
            if (sceneObject is ISceneAssembly assembly)
            {
                return new SceneAssemblyDataViewModel(assembly);
            }
            else if (sceneObject is ISceneEntity entity)
            {
                return new SceneEntityDataViewModel(entity);
            }
            else
            {
                return default(ISceneObjectTypeDataVM);
            }
        }

        private void OnCurrentSceneChanged(object sender, CurrentSceneChangedEventArgs e)
        {
            sceneManager.CurrentScene.SelectionManager.SelectionChanged += OnSelectionChanged;
            ProcessSelection();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessSelection();
        }

        private SceneObjectDataViewModel? sceneObjectBaseDataVM;
        private ISceneObjectTypeDataVM? sceneObjectTypeDataVM;
    }
}
