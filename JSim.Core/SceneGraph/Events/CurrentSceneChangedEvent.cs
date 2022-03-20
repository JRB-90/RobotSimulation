namespace JSim.Core.SceneGraph
{
    public delegate void CurrentSceneChangedEventHandler(object sender, CurrentSceneChangedEventArgs e);

    public class CurrentSceneChangedEventArgs : EventArgs
    {
        public CurrentSceneChangedEventArgs(IScene newScene)
        {
            NewScene = newScene;
        }

        public IScene NewScene { get; }
    }
}
