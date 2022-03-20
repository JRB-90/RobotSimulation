namespace JSim.Core.SceneGraph
{
    public delegate void SceneStructureChangedEventHandler(object sender, SceneStructureChangedEventArgs e);

    public class SceneStructureChangedEventArgs : EventArgs
    {
        public SceneStructureChangedEventArgs(ISceneAssembly rootAssembly)
        {
            RootAssembly = rootAssembly;
        }

        public ISceneAssembly RootAssembly { get; }
    }
}
