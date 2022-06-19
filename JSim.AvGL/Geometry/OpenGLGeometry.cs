using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.AvGL
{
    /// <summary>
    /// Provides an OpenGL compatible implementation of the geometry object.
    /// Manages the conversion of JSim geometry data into OpenGL GPU resources
    /// and their subsequent release.
    /// </summary>
    public class OpenGLGeometry : GeometryBase, IDisposable
    {
        readonly IGlContextManager glContextManager;

        public OpenGLGeometry(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator,
            IGlContextManager glContextManager)
          :
            base(
                nameRepository,
                messageCollator,
                creator)
        {
            this.glContextManager = glContextManager;
            VAO = new VAO();
        }

        public OpenGLGeometry(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator,
            IGlContextManager glContextManager,
            IGeometry? parentGeometry)
          :
            base(
                nameRepository,
                messageCollator,
                creator,
                parentGeometry)
        {
            this.glContextManager = glContextManager;
            VAO = new VAO();
        }

        /// <summary>
        /// Vertex Array Object (VAO) which contains the handles to the
        /// OpenGL GPU resources that contain the uploaded geometry data.
        /// </summary>
        public VAO VAO { get; private set; }

        public void Dispose()
        {
            if (gl != null)
            {
                VAO.DeleteVAO(gl, VAO);
            }
        }

        /// <summary>
        /// Rebuilds the GPU resources from the geometry primitives data.
        /// </summary>
        protected override void Rebuild()
        {
            glContextManager.RunOnResourceContext(
                (g) =>
                {
                    gl = new GLBindingsInterface(g);

                    var newVao =
                        VAO.CreateVAO(
                            gl,
                            Vertices.ToArray(),
                            Indices
                                .Select(v => (ushort)v)
                                .ToArray()
                        );

                    VAO.DeleteVAO(gl, VAO);
                    VAO = newVao;
                }
            );
        }

        private GLBindingsInterface? gl;
    }
}
