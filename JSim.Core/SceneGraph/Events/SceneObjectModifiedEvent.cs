namespace JSim.Core.SceneGraph
{
    public delegate void SceneObjectModifiedEventHandler(object sender, SceneObjectModifiedEventArgs e);

    public class SceneObjectModifiedEventArgs : EventArgs
    {
        public SceneObjectModifiedEventArgs(ISceneObject sceneObject)
        {
            SceneObject = sceneObject;
        }

        public ISceneObject SceneObject { get; }
    }
}
