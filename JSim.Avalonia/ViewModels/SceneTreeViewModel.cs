using Avalonia.Controls;
using Avalonia.Input;
using JSim.Avalonia.Models;
using JSim.Avalonia.Shared;
using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.ViewModels
{
    public class SceneTreeViewModel : ViewModelBase
    {
        readonly ISceneManager sceneManager;
        readonly Shared.InputManager inputManager;
        readonly DialogManager dialogManager;

        public SceneTreeViewModel(
            ISceneManager sceneManager,
            Shared.InputManager inputManager,
            DialogManager dialogManager)
        {
            this.sceneManager = sceneManager;
            this.inputManager = inputManager;
            this.dialogManager = dialogManager;

            sceneModels =
                new List<SceneModel>()
                {
                    new SceneModel(sceneManager.CurrentScene)
                };

            sceneManager.CurrentSceneChanged += OnCurrentSceneChanged;
            inputManager.PointerMoved += OnPointerMoved;
            inputManager.PointerReleased += OnPointerReleased;
        }

        internal IReadOnlyCollection<SceneModel> SceneModels
        {
            get => sceneModels;
            private set => this.RaiseAndSetIfChanged(ref sceneModels, value, nameof(SceneModels));
        }

        public void SceneObjectSelectionChanged(global::Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (e.Source is TreeView tree &&
                tree.SelectedItems != null)
            {
                if (tree.SelectedItems.Count == 0)
                {
                    sceneManager.CurrentScene.SelectionManager.ResetSelection();
                }
                else if (tree.SelectedItems.Count == 1)
                {
                    if (tree.SelectedItems[0] is SceneObjectModelBase sceneObject)
                    {
                        sceneManager.CurrentScene.SelectionManager.SetSingleSelection(
                            sceneObject.SceneObject
                        );
                    }
                    else
                    {
                        sceneManager.CurrentScene.SelectionManager.ResetSelection();
                    }
                }
                else
                {
                    var selectedSceneObjects = new List<ISceneObject>();

                    foreach (var item in tree.SelectedItems)
                    {
                        if (tree.SelectedItems[0] is SceneObjectModelBase sceneObject)
                        {
                            selectedSceneObjects.Add(sceneObject.SceneObject);
                        }
                    }

                    sceneManager.CurrentScene.SelectionManager.SetMultiSelection(
                        selectedSceneObjects
                    );
                }
            }
        }

        public void SceneObjectPointerPressed(PointerPressedEventArgs e)
        {
            if (IsLeftClicked(e) &&
                e.Source is Control control &&
                control.DataContext is SceneObjectModelBase sceneObject)
            {
                EnableDrag(sceneObject);
            }
            else
            {
                DisableDrag();
            }
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            CheckReleasedAction(sender, e);
            DisableDrag();
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (IsDragEnabled)
            {
                CheckDragAction(sender, e);
            }
        }

        private void OnCurrentSceneChanged(object sender, CurrentSceneChangedEventArgs e)
        {
            SceneModels =
                new List<SceneModel>()
                {
                    new SceneModel(sceneManager.CurrentScene)
                };
        }

        private bool IsDragEnabled =>
            draggedObject != null;

        private bool IsLeftClicked(PointerPressedEventArgs e)
        {
            if (e.Source is Control control)
            {
                var props = e.GetCurrentPoint(control).Properties;

                return props.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed;
            }
            else
            {
                return false;
            }
        }

        private void EnableDrag(SceneObjectModelBase draggedObject)
        {
            this.draggedObject = draggedObject;
            inputManager.Cursor = new Cursor(StandardCursorType.No);
        }

        private void DisableDrag()
        {
            draggedObject = null;
            inputManager.Cursor = Cursor.Default;
        }

        private void CheckDragAction(object? sender, PointerEventArgs e)
        {
            if (sender is Control sourceControl)
            {
                var pos = e.GetPosition(sourceControl);
                var element = sourceControl.InputHitTest(pos);

                if (element is Control control)
                {
                    if (control.DataContext is SceneModel)
                    {
                        inputManager.Cursor = new Cursor(StandardCursorType.DragMove);
                    }
                    else if (control.DataContext is SceneAssemblyModel)
                    {
                        inputManager.Cursor = new Cursor(StandardCursorType.DragMove);
                    }
                    else
                    {
                        inputManager.Cursor = new Cursor(StandardCursorType.No);
                    }
                }
            }
        }

        private void CheckReleasedAction(object? sender, PointerReleasedEventArgs e)
        {
            if (sender is Control sourceControl)
            {
                var pos = e.GetPosition(sourceControl);
                var element = sourceControl.InputHitTest(pos);

                if (element is Control control &&
                    control.DataContext != draggedObject)
                {
                    if (control.DataContext is SceneModel scene)
                    {
                        draggedObject?.Move(scene.Scene.Root);
                    }
                    else if (control.DataContext is SceneAssemblyModel sceneAssembly)
                    {
                        draggedObject?.Move(sceneAssembly.Assembly);
                    }
                }
            }
        }

        private IReadOnlyCollection<SceneModel> sceneModels;
        private SceneObjectModelBase? draggedObject;
    }
}
