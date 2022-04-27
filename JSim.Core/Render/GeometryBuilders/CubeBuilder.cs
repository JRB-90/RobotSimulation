using JSim.Core.Maths;

namespace JSim.Core.Render.GeometryBuilders
{
    public static class CubeBuilder
    {
        public static Tuple<IReadOnlyList<Vertex>, IReadOnlyList<uint>> Build(double size)
        {
            return Build(size, size, size);
        }

        public static Tuple<IReadOnlyList<Vertex>, IReadOnlyList<uint>> Build(
            double width,
            double length,
            double height)
        {
            double halfWidth = width / 2.0;
            double halfLength = length / 2.0;
            double halfHeight = height / 2.0;

            //var vertices =
            //    new List<Vertex>
            //    {
            //        // Near plane
            //        new Vertex(0, new Vector3D(halfWidth, halfLength, halfLength)),  // 
            //        new Vertex(1, new Vector3D(halfWidth, -halfLength, halfLength)),
            //        new Vertex(2, new Vector3D(-halfWidth, halfLength, halfLength)),
            //        new Vertex(3, new Vector3D(-halfWidth, -halfLength, halfLength)),

            //        // Far plane
            //        new Vertex(4, new Vector3D(halfWidth, halfLength, -halfLength)),
            //        new Vertex(5, new Vector3D(halfWidth, -halfLength, -halfLength)),
            //        new Vertex(6, new Vector3D(-halfWidth, -halfLength, -halfLength)),
            //        new Vertex(7, new Vector3D(-halfWidth, halfLength, -halfLength))
            //    };

            //var indices =
            //    new List<uint>
            //    {
            //        0, 1, 2, 1, 3, 2,

            //        0, 1, 2, 1, 3, 2,
            //    };

            //VertexUtil.CalculateVertexNormals(vertices, indices);

            var vertices =
                new List<Vertex>
                {
                    // Near plane
                    new Vertex(0, new Vector3D(-halfWidth, -halfLength, halfHeight)),   // TL
                    new Vertex(1, new Vector3D(halfWidth, -halfLength, halfHeight)),    // TR
                    new Vertex(2, new Vector3D(halfWidth, -halfLength, -halfHeight)),   // BR
                    new Vertex(3, new Vector3D(-halfWidth, -halfLength, -halfHeight)),  // BL

                    // Far plane
                    new Vertex(0, new Vector3D(halfWidth, halfLength, halfHeight)),     // TL
                    new Vertex(1, new Vector3D(-halfWidth, halfLength, halfHeight)),    // TR
                    new Vertex(2, new Vector3D(-halfWidth, halfLength, -halfHeight)),   // BR
                    new Vertex(3, new Vector3D(halfWidth, halfLength, -halfHeight)),    // BL
                };

            var indices =
                new List<uint>
                {
                    0, 1, 3, 3, 1, 2,   // Font face
                    1, 0, 5, 5, 4, 1,   // Top face
                    3, 2, 6, 6, 2, 7,   // Bottom face
                    1, 4, 2, 2, 4, 7,   // Right face
                    6, 5, 0, 0, 3, 6,    // Left face
                    5, 6, 4, 4, 6, 7,   // Back face
                };

            return
                new Tuple<IReadOnlyList<Vertex>, IReadOnlyList<uint>>(
                    vertices,
                    indices
                );
        }
    }
}
