using Castle.Core.Logging;
using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
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

        public void Dispose()
        {
            VboUtils.DeleteVbo(VBO);
        }

        public Vbo VBO { get; private set; }

        protected override void Rebuild()
        {
            glContextManager.RunOnResourceContext(
                () => 
                VBO =
                    VboUtils.CreateVbo(
                        Vertices.ToArray(),
                        Indices.ToArray()
                    )
            );
        }
    }
}
