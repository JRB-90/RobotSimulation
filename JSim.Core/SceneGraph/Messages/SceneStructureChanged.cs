namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Message to notify that a scenes structure has changed.
    /// </summary>
    public class SceneStructureChanged
    {
        public SceneStructureChanged(ISceneAssembly rootAssembly)
        {
            RootAssembly = rootAssembly;
        }

        public ISceneAssembly RootAssembly { get; }
    }
}
