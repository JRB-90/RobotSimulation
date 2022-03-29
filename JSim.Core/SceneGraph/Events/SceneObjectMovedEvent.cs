namespace JSim.Core.SceneGraph
{
    public delegate void SceneObjectMovedEventHandler(object sender, SceneObjectMovedEventArgs e);

    public class SceneObjectMovedEventArgs : EventArgs
    {
        public SceneObjectMovedEventArgs(ISceneObject sceneObject)
        {
            SceneObject = sceneObject;
        }

        public ISceneObject SceneObject { get; }
    }
}
