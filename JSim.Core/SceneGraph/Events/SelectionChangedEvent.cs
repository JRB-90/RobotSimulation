namespace JSim.Core.SceneGraph
{
    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);

    public class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs()
        {
        }
    }
}
