using JSim.Core.Maths;
using MathNet.Numerics.LinearAlgebra;
using System.Runtime.InteropServices;

namespace JSim.Core.Render
{
    /// <summary>
    /// Struct representing a drawable vertex in space.
    /// </summary>
    /// <remarks>
    /// Contains the position, vertex normal and texture coordinate of the vertex.
    /// By using this class to store vertex data, it can be easily serialised.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        private VertData3D position;
        private VertData3D normal;
        private VertData4D color;
        private VertData2D texCoord;

        /// <summary>
        /// Vertex position.
        /// </summary>
        public Vector3D Position
        {
            get => new Vector3D(position.x, position.y, position.z);
            set => position = new VertData3D(value);
        }

        /// <summary>
        /// Vertex normal.
        /// </summary>
        public Vector3D Normal
        {
            get => new Vector3D(normal.x, normal.y, normal.z);
            set => normal = new VertData3D(value);
        }

        /// <summary>
        /// Vertex color.
        /// </summary>
        public Vector<double> Color
        {
            get
            {
                double[] values = { color.x, color.y, color.y, color.z };
                Vector<double> vector = Vector<double>.Build.Dense(values);

                return vector;
            }
            set => color = new VertData4D(value);
        }

        /// <summary>
        /// Vertex texture coordinate.
        /// </summary>
        public Vector2D TexCoord
        {
            get => new Vector2D(texCoord.x, texCoord.y);
            set => texCoord = new VertData2D(value);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(0.0, 0.0, 0.0);
            this.color = new VertData4D(-1.0, -1.0, -1.0, -1.0);
            this.texCoord = new VertData2D(0.0, 0.0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="normal">Normal to the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position, 
            Vector3D normal)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(normal);
            this.color = new VertData4D(-1.0, -1.0, -1.0, -1.0);
            this.texCoord = new VertData2D(0.0, 0.0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="normal">Normal to the vertex.</param>
        /// /// <param name="color">Color attached to the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position, 
            Vector3D normal, 
            Color color)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(normal);
            this.color = new VertData4D(color);
            this.texCoord = new VertData2D(0.0, 0.0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="normal">Normal to the vertex.</param>
        /// <param name="texCoord">Texture coordinate of the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position, 
            Vector3D normal, 
            Vector2D texCoord)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(normal);
            this.color = new VertData4D(-1.0, -1.0, -1.0, -1.0);
            this.texCoord = new VertData2D(texCoord);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="color">Color attached to the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position, 
            Color color)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(0.0, 0.0, 0.0);
            this.color = new VertData4D(color);
            this.texCoord = new VertData2D(0.0, 0.0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="color">Color attached to the vertex.</param>
        /// <param name="texCoord">Texture coordinate of the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position, 
            Color color, 
            Vector2D texCoord)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(0.0, 0.0, 0.0);
            this.color = new VertData4D(color);
            this.texCoord = new VertData2D(texCoord);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="texCoord">Texture coordinate of the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position,
            Vector2D texCoord)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(0.0, 0.0, 0.0);
            this.color = new VertData4D(-1.0, -1.0, -1.0, -1.0);
            this.texCoord = new VertData2D(texCoord);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertexID">Index of the vertex.</param>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="normal">Normal to the vertex.</param>
        /// <param name="color">Color attached to the vertex.</param>
        /// <param name="texCoord">Texture coordinate of the vertex.</param>
        public Vertex(
            float vertexID, 
            Vector3D position, 
            Vector3D normal, 
            Color color, 
            Vector2D texCoord)
        {
            this.position = new VertData3D(position);
            this.normal = new VertData3D(normal);
            this.color = new VertData4D(color);
            this.texCoord = new VertData2D(texCoord);
        }

        /// <summary>
        /// Size of a single vertex struct in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 
            Marshal.SizeOf(new VertData3D()) +
            Marshal.SizeOf(new VertData3D()) +
            Marshal.SizeOf(new VertData4D()) +
            Marshal.SizeOf(new VertData2D());
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VertData2D
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public VertData2D(
            double x, 
            double y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vector">Point coordinate.</param>
        public VertData2D(Vector2D vector)
        {
            x = (float)vector.X;
            y = (float)vector.Y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VertData3D
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// Z coordinate.
        /// </summary>
        public float z;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public VertData3D(
            double x, 
            double y, 
            double z)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vector">Vector coordinate.</param>
        public VertData3D(Vector3D vector)
        {
            x = (float)vector.X;
            y = (float)vector.Y;
            z = (float)vector.Z;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VertData4D
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// Z coordinate.
        /// </summary>
        public float z;

        /// <summary>
        /// W coordinate.
        /// </summary>
        public float w;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public VertData4D(
            double x, 
            double y, 
            double z, 
            double w)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;
            this.w = (float)w;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">Vertex color.</param>
        public VertData4D(Vector<double> vector)
        {
            if (vector.L2Norm() != 4)
            {
                throw new ArgumentException("Vector not of size 4");
            }

            x = (float)vector[0];
            y = (float)vector[1];
            z = (float)vector[2];
            w = (float)vector[3];
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">Vertex color.</param>
        public VertData4D(Color color)
        {
            x = (float)color.R / 255.0f;
            y = (float)color.G / 255.0f;
            z = (float)color.B / 255.0f;
            w = (float)color.A / 255.0f;
        }
    }

    /// <summary>
    /// Static util class providing static vertex helper functions.
    /// </summary>
    public static class VertexUtil
    {
        /// <summary>
        /// Calculate the vertex normals for a list of vertices.
        /// </summary>
        /// <param name="vertices">List of vertices to calculate from.</param>
        /// <param name="indices">Drawing order of the vertices.</param>
        public static void CalculateVertexNormals(List<Vertex> vertices, List<uint> indices)
        {
            CalculateVertexNormals(vertices.ToArray(), indices.ToArray());
        }

        /// <summary>
        /// Calculate the vertex normals for a list of vertices.
        /// </summary>
        /// <param name="vertices">List of vertices to calculate from.</param>
        /// <param name="indices">Drawing order of the vertices.</param>
        public static void CalculateVertexNormals(Vertex[] vertices, uint[] indices)
        {
            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3D v1 = vertices[indices[i + 1]].Position - vertices[indices[i]].Position;
                Vector3D v2 = vertices[indices[i + 2]].Position - vertices[indices[i]].Position;
                Vector3D normal = -1.0 * v1.Cross(v2);
                normal.Normalize();

                vertices[indices[i]].Normal += normal;
                vertices[indices[i + 1]].Normal += normal;
                vertices[indices[i + 2]].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].Normal.Length.Equals(float.NaN))
                {
                    Vector3D norm = new Vector3D(0.0, 0.0, 0.0);
                    vertices[i].Normal = norm;
                    continue;
                }
                vertices[i].Normal.Normalize();
            }
        }

        /// <summary>
        /// Rough estimation of texture coordinates for a list of vertices.
        /// </summary>
        /// <param name="vertices">List of vertices to calculate from.</param>
        /// <param name="indices">Drawing order of the vertices.</param>
        public static void CalculateVertexTexCoords(
            List<Vertex> vertices, 
            List<uint> indices)
        {
            CalculateVertexTexCoords(
                vertices.ToArray(), 
                indices.ToArray()
            );
        }

        /// <summary>
        /// Rough estimation of texture coordinates for a list of vertices.
        /// </summary>
        /// <param name="vertices">List of vertices to calculate from.</param>
        /// <param name="indices">Drawing order of the vertices.</param>
        public static void CalculateVertexTexCoords(
            Vertex[] vertices, 
            uint[] indices)
        {
            double lowestX, lowestZ;
            double highestX, highestZ;
            lowestX = lowestZ = double.PositiveInfinity;
            highestX = highestZ = double.NegativeInfinity;

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].Position.X < lowestX)
                {
                    lowestX = vertices[i].Position.X;
                }

                if (vertices[i].Position.X > highestX)
                {
                    highestX = vertices[i].Position.X;
                }

                if (vertices[i].Position.X < lowestZ)
                {
                    lowestZ = vertices[i].Position.Z;
                }

                if (vertices[i].Position.X > highestZ)
                {
                    highestZ = vertices[i].Position.Z;
                }
            }

            double rangeX = (lowestX - highestX) * -1.0;
            double rangeZ = (lowestZ - highestZ) * -1.0;
            double offsetX = 0.0 - lowestX;
            double offsetZ = 0.0 - lowestZ;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2D tex =
                    new Vector2D(
                        (vertices[i].Position.X + offsetX) / rangeX,
                        (vertices[i].Position.Z + offsetZ) / rangeZ
                    );

                vertices[i].TexCoord = tex;
            }
        }
    }
}
