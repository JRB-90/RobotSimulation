using JSim.Core.Render;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behaviour of all ISceneEntity objects.
    /// Scene Entities represent an 'entity' in the scene, i.e. a robot
    /// or cell guarding etc. From the high level view of the scene, it
    /// is easier for a user to create and interact with entites that
    /// represent a more complex item, rather than have the individual
    /// components of an entity (joints, geometry, behaviors) cluttering
    /// up the scene graph.
    /// </summary>
    public interface ISceneEntity : ISceneObject
    {
        /// <summary>
        /// Gets the geometry container for this entity, which contains the
        /// entities geometry tree.
        /// </summary>
        IGeometryContainer GeometryContainer { get; }
    }
}
