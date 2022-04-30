using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// Provides an OpenTK compatible implementation of the geometry object.
    /// Manages the conversion of JSim geometry data into OpenTK GPU resources
    /// and their subsequent release.
    /// </summary>
    public class OpenTKGeometry : GeometryBase, IDisposable
    {
        readonly IGlContextManager glContextManager;

        public OpenTKGeometry(
            INameRepository nameRepository, 
            IGeometryCreator creator,
            IGlContextManager glContextManager)
          :
            base(
                nameRepository,
                creator)
        {
            this.glContextManager = glContextManager;
            VBO = new Vbo();
        }

        public OpenTKGeometry(
            INameRepository nameRepository,
            IGeometryCreator creator,
            IGlContextManager glContextManager,
            IGeometry? parentGeometry)
          :
            base(
                nameRepository,
                creator,
                parentGeometry)
        {
            this.glContextManager = glContextManager;
            VBO = new Vbo();
        }

        /// <summary>
        /// Disposes this object and releases any GPU resources.
        /// </summary>
        public void Dispose()
        {
            VboUtils.DeleteVbo(VBO);
        }

        /// <summary>
        /// Vertex Buffer Object (VBO) which contains the handles to the
        /// OpenGL GPU resources that contain the uploaded geometry data.
        /// </summary>
        public Vbo VBO { get; private set; }

        /// <summary>
        /// Rebuilds the GPU resources from the geometry primitives data.
        /// </summary>
        protected override void Rebuild()
        {
            glContextManager.RunOnResourceContext(
                () =>
                {
                    var newVbo =
                        VboUtils.CreateVbo(
                            Vertices.ToArray(),
                            Indices.ToArray()
                        );

                    VboUtils.DeleteVbo(VBO);
                    VBO = newVbo;
                }
            );
        }
    }
}
