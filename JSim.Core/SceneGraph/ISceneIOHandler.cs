namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Defines the behaviour of all scene graph saving/loading implementations.
    /// </summary>
    public interface ISceneIOHandler
    {
        /// <summary>
        /// Loads a scene from disk.
        /// </summary>
        /// <param name="path">File path to load the scene file from.</param>
        /// <returns>Loaded scene.</returns>
        IScene LoadSceneFromFile(string path);

        /// <summary>
        /// Saves a scene to disk.
        /// </summary>
        /// <param name="scene">Scene to save.</param>
        /// <param name="path">File path to save the scene file to.</param>
        void SaveSceneToFile(IScene scene, string path);
    }
}
