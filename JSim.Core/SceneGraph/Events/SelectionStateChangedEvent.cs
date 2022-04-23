namespace JSim.Core.SceneGraph
{
    public delegate void SelectionStateChangedEventHandler(object sender, SelectionStateChangedEventArgs e);

    public class SelectionStateChangedEventArgs : EventArgs
    {
        public SelectionStateChangedEventArgs(bool isSelected)
        {
            IsSelected = isSelected;
        }

        public bool IsSelected { get; }
    }
}
