using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    public class OpenTKGeometry : GeometryBase
    {
        public OpenTKGeometry(
            INameRepository nameRepository, 
            IGeometryCreator creator)
          :
            base(
                nameRepository,
                creator)
        {
        }

        public Vbo? VBO { get; private set; }

        protected override void Rebuild()
        {
            VBO = 
                VboUtils.CreateVbo(
                    Vertices.ToArray(), 
                    Indices.ToArray()
                );
        }
    }
}
