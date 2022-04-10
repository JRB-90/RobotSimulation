using JSim.Core.SceneGraph.Events;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of the selection manager.
    /// </summary>
    public class SelectionManager : ISelectionManager
    {
        public SelectionManager()
        {
            SelectionState = SelectionState.NoSelection;
            SelectedObject = null;
            SelectedObjects = null;
        }

        public SelectionState SelectionState { get; private set; }

        public ISceneObject? SelectedObject { get; private set; }

        public IReadOnlyCollection<ISceneObject>? SelectedObjects { get; private set; }

        public event SelectionChangedEventHandler? SelectionChanged;

        public void ResetSelection()
        {
            SelectionState = SelectionState.NoSelection;
            SelectedObject = null;
            SelectedObjects = null;
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs());
        }

        public void SetSingleSelection(ISceneObject selectedObject)
        {
            SelectionState = SelectionState.SingleSelection;
            SelectedObject = selectedObject;
            SelectedObjects = null;
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs());
        }

        public void SetMultiSelection(IReadOnlyCollection<ISceneObject> selectedObjects)
        {
            SelectionState = SelectionState.MultiSelection;
            SelectedObject = null;
            SelectedObjects = selectedObjects;
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs());
        }
    }
}
