using JSim.Core.Maths;
using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define the behaviour of geomerty, which is an object
    /// that can be rendered. Geometry can be nested together to build
    /// more complex models.
    /// 
    /// It stores the vertex, material and positonal data that allow a
    /// rendering engine to draw it. An implementation of IGeometry must
    /// match the expected type in the IRenderingEngine implementation.
    /// This could include, for example, an OpenGL implementation storing
    /// opengl handles for gpu resources.
    /// </summary>
    public interface IGeometry : IEnumerable<IGeometry>
    {
        /// <summary>
        /// Unique id of this geometry instance.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Name of the geometry, unique within the geometry tree.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Flag to indicate if the geometry should not be rendered.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Flag to indicate if the geometry should be highlighted.
        /// </summary>
        bool IsHighlighted { get; set; }

        /// <summary>
        /// Tracks the selection state of the geometry.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Parent node this geometry is attached to.
        /// Null if root of tree or unattached.
        /// </summary>
        IGeometry? ParentGeometry { get; }

        /// <summary>
        /// Transform relative to the parent of the geometry tree.
        /// </summary>
        Transform3D WorldFrame { get; set; }

        /// <summary>
        /// Transform relative to the parent node..
        /// </summary>
        Transform3D LocalFrame { get; set; }

        /// <summary>
        /// Gets a list of all the child geometry attached to this node.
        /// </summary>
        IReadOnlyCollection<IGeometry> Children { get; }

        /// <summary>
        /// Gets the geometries vertices.
        /// </summary>
        IReadOnlyList<Vertex> Vertices { get; }

        /// <summary>
        /// Gets the geometries drawing indices.
        /// </summary>
        IReadOnlyList<uint> Indices { get; }

        /// <summary>
        /// Assigns a Material to this geometry.
        /// </summary>
        IMaterial Material { get; set; }

        /// <summary>
        /// Gets the type of the geometry, which is used to signify how it should
        /// be drawn by a renderer.
        /// </summary>
        GeometryType GeometryType { get; set; }

        /// <summary>
        /// Event fired when the geometry has been rebuilt.
        /// </summary>
        event GeometryRebuiltEventHandler? GeometryRebuilt;

        /// <summary>
        /// Event fired when the geometries properties have been modified.
        /// </summary>
        event GeometryModifiedEventHandler? GeometryModified;

        /// <summary>
        /// Event fired when the selection state of this object has changed.
        /// </summary>
        event SelectionStateChangedEventHandler? SelectionStateChanged;

        /// <summary>
        /// Sets the vertices and indices of the geometry.
        /// </summary>
        /// <param name="vertices">Vertex data.</param>
        /// <param name="indices">Drawing indices.</param>
        void SetDrawingData(
            IReadOnlyList<Vertex> vertices,
            IReadOnlyList<uint> indices
        );

        /// <summary>
        /// Moves the geometry to a new node.
        /// </summary>
        /// <param name="newParent">New geometry to attach this node to.</param>
        /// <returns>True if move was successful.</returns>
        public bool MoveGeometry(IGeometry newParent);

        /// <summary>
        /// Attaches a geometry to this nodes children.
        /// </summary>
        /// <param name="geometry">Geometry to attach to this node.</param>
        /// <returns>Returns true if successful.</returns>
        bool AttachGeometry(IGeometry geometry);

        /// <summary>
        /// Detaches a geometry from this nodes children.
        /// </summary>
        /// <param name="geometry">Geometry to remove from this node.</param>
        /// <returns>Returns true if successfully removed.</returns>
        bool DetachGeometry(IGeometry geometry);

        /// <summary>
        /// Creates a new geometry object childed to this node.
        /// </summary>
        /// <returns>Newly instantiated geometry object.</returns>
        IGeometry CreateChildGeometry();

        /// <summary>
        /// Creates a new geometry object childed to this node.
        /// </summary>
        /// <param name="name">Name to assign the geometry object. Must be unique to tree.</param>
        /// <returns>Newly instantiated geometry object.</returns>
        IGeometry CreateChildGeometry(string name);

        /// <summary>
        /// Forces the geometry to recalculate it's world position from it's parent.
        /// </summary>
        void RecalculateWorldPosition(Transform3D parentWorldPosition);
    }
}
