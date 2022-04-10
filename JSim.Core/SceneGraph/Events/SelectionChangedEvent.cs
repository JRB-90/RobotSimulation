namespace JSim.Core.SceneGraph.Events
{
    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);

    public class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs()
        {
        }
    }
}
