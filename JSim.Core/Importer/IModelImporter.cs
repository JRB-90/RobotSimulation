using JSim.Core.SceneGraph;

namespace JSim.Core.Importer
{
    /// <summary>
    /// Interface defining the behaviour of all CAD loading implementations.
    /// </summary>
    public interface IModelImporter
    {
        /// <summary>
        /// Attempts to load the given file from disk and construct a scene entity from it.
        /// </summary>
        /// <param name="path">Path to the model file.</param>
        /// <param name="parent">Parent assembly to attach this model to.</param>
        /// <returns>Constructed scene entity containing the model geometry.</returns>
        ISceneObject LoadModel(string path, ISceneAssembly parent);
    }
}
