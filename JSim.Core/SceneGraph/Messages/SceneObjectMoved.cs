namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Message to notify that a scene object has moved.
    /// </summary>
    public class SceneObjectMoved
    {
        public SceneObjectMoved(ISceneObject sceneObject)
        {
            SceneObject = sceneObject;
        }

        public ISceneObject SceneObject { get; }
    }
}
