using JSim.Core.SceneGraph.Events;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Selection managers monitor which scene objects in a scene
    /// have been selected and when the selection changes.
    /// </summary>
    public interface ISelectionManager
    {
        /// <summary>
        /// Indicates what the current selection state is.
        /// </summary>
        SelectionState SelectionState { get; }

        /// <summary>
        /// If in single selection state, gets the selected scene object.
        /// </summary>
        public ISceneObject? SelectedObject { get; }

        /// <summary>
        /// If in multi selection state, gets the selected scene objects.
        /// </summary>
        public IReadOnlyCollection<ISceneObject>? SelectedObjects { get; }

        /// <summary>
        /// Event fired when the scene objects selected has changed.
        /// </summary>
        event SelectionChangedEventHandler? SelectionChanged;

        /// <summary>
        ///  Resets the current selection so that no objects are selected.
        /// </summary>
        void ResetSelection();

        /// <summary>
        /// Sets a single object to be selected.
        /// </summary>
        /// <param name="selectedObject">Scene object that is selected.</param>
        void SetSingleSelection(ISceneObject selectedObject);

        /// <summary>
        /// Sets multiple objects to be selected.
        /// </summary>
        /// <param name="selectedObjects">Scene objects that are selected.</param>
        void SetMultiSelection(IReadOnlyCollection<ISceneObject> selectedObjects);
    }
}
