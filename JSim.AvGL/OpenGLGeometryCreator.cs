using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.AvGL
{
    /// <summary>
    /// OpenGL compatible implementation of a geometry creator.
    /// Provides correct dependencies when creating OpenGL geometry objects.
    /// </summary>
    public class OpenGLGeometryCreator : IGeometryCreator
    {
        readonly INameRepository nameRepository;
        readonly IOpenGLGeometryFactory geometryFactory;
        readonly IGlContextManager glContextManager;

        public OpenGLGeometryCreator(
            INameRepositoryFactory nameRepositoryFactory,
            IOpenGLGeometryFactory geometryFactory,
            IGlContextManager glContextManager)
        {
            this.geometryFactory = geometryFactory;
            this.glContextManager = glContextManager;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Creates a new OpenTKGeometry object.
        /// </summary>
        /// <param name="parentGeometry">Parent to attach this geometry node to.</param>
        /// <returns>OpenTKGeometry object.</returns>
        public IGeometry CreateGeometry(IGeometry? parentGeometry)
        {
            return
                geometryFactory.CreateGeometry(
                    nameRepository,
                    this,
                    glContextManager,
                    parentGeometry
                );
        }
    }
}
