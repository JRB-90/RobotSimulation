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

            if (SelectedObject != null)
            {
                SelectedObject.IsSelected = false;
            }
            SelectedObject = null;

            if (SelectedObjects != null)
            {
                foreach (var so in SelectedObjects)
                {
                    so.IsSelected = false;
                }
            }
            SelectedObjects = null;

            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs());
        }

        public void SetSingleSelection(ISceneObject selectedObject)
        {
            SelectionState = SelectionState.SingleSelection;

            if (SelectedObject != null)
            {
                SelectedObject.IsSelected = false;
            }
            SelectedObject = selectedObject;
            SelectedObject.IsSelected = true;

            if (SelectedObjects != null)
            {
                foreach (var so in SelectedObjects)
                {
                    so.IsSelected = false;
                }
            }
            SelectedObjects = null;

            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs());
        }

        public void SetMultiSelection(IReadOnlyCollection<ISceneObject> selectedObjects)
        {
            SelectionState = SelectionState.MultiSelection;

            if (SelectedObject != null)
            {
                SelectedObject.IsSelected = false;
            }
            SelectedObject = null;

            if (SelectedObjects != null)
            {
                foreach (var so in SelectedObjects)
                {
                    so.IsSelected = false;
                }
            }

            SelectedObjects = selectedObjects;
            foreach (var so in SelectedObjects)
            {
                so.IsSelected = true;
            }

            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs());
        }
    }
}
