namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Message to notifiy of a scene object being modified.
    /// </summary>
    public class SceneObjectModified
    {
        public SceneObjectModified(ISceneObject sceneObject)
        {
            SceneObject = sceneObject;
        }

        public ISceneObject SceneObject { get; }
    }
}
