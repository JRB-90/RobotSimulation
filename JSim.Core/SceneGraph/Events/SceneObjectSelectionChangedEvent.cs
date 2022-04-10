namespace JSim.Core.SceneGraph
{
    public delegate void SceneObjectSelectionChangedEventHandler(object sender, SceneObjectSelectionChangedEventArgs e);

    public class SceneObjectSelectionChangedEventArgs : EventArgs
    {
        public SceneObjectSelectionChangedEventArgs(bool isSelected)
        {
            IsSelected = isSelected;
        }

        public bool IsSelected { get; }
    }
}
