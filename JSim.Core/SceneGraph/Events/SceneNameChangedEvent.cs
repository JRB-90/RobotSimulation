namespace JSim.Core.SceneGraph
{
    public delegate void SceneNameChangedEventHandler(object sender, SceneNameChangedEventArgs e);

    public class SceneNameChangedEventArgs : EventArgs
    {
        public SceneNameChangedEventArgs(string newName)
        {
            NewName = newName;
        }

        public string NewName { get; }
    }
}
