namespace JSim.Core.Common
{
    public delegate void SelectionStateChangedEventHandler(object sender, SelectionStateChangedEventArgs e);
    public class SelectionStateChangedEventArgs : EventArgs
    {
        public SelectionStateChangedEventArgs(
            ISelectable selectableObject,
            bool newSelectionState)
        {
            SelectableObject = selectableObject;
            NewSelectionState = newSelectionState;
        }

        public ISelectable SelectableObject { get; }
        public bool NewSelectionState { get; }
    }
}
