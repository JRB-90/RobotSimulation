﻿namespace JSim.Core.SceneGraph
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
        string Name { get; }
    }
}
