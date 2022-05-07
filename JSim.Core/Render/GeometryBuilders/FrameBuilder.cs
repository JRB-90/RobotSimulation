using JSim.Core.Maths;

namespace JSim.Core.Render.GeometryBuilders
{
    public static class FrameBuilder
    {
        public static Tuple<IReadOnlyList<Vertex>, IReadOnlyList<uint>> BuildSimple(double size)
        {
            var vertices =
                new Vertex[]
                {
                    new Vertex(0, new Vector3D(0, 0, 0)),
                    new Vertex(1, new Vector3D(size, 0, 0)),
                    new Vertex(2, new Vector3D(0, 0, 0)),
                    new Vertex(3, new Vector3D(0, size, 0)),
                    new Vertex(4, new Vector3D(0, 0, 0)),
                    new Vertex(5, new Vector3D(0, 0, size))
                };

            var indices =
                new uint[]
                {
                    0, 1, 2, 3, 4, 5
                };

            return
                new Tuple<IReadOnlyList<Vertex>, IReadOnlyList<uint>>(
                    vertices,
                    indices
                );
        }
    }
}
