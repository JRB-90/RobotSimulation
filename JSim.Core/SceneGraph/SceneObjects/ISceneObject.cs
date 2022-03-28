using JSim.Core.Maths;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface defining the behaviour off all objects that reside in
    /// a scene graph.
    /// </summary>
    public interface ISceneObject
    {
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Unique name for the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Parent scene assembly that this object is attached to.
        /// Null if it is the root node.
        /// </summary>
        ISceneAssembly? ParentAssembly { get; }

        /// <summary>
        /// The position of the object in world coordinates.
        /// </summary>
        Transform3D WorldFrame { get; set; }

        /// <summary>
        /// The position of the object in local coordinates, i.e. relative
        /// to it's parent assembly.
        /// </summary>
        Transform3D LocalFrame { get; set; }

        /// <summary>
        /// Event fired when this scene object has been modified.
        /// </summary>
        event SceneObjectModifiedEventHandler SceneObjectModified;

        /// <summary>
        /// Moves the scene object to a new assembly.
        /// </summary>
        /// <param name="newAssembly">New assembly to attach this acene object to.</param>
        /// <returns>True if move was successful.</returns>
        bool MoveAssembly(ISceneAssembly newAssembly);
    }
}
