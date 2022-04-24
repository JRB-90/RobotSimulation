using OpenTK.Graphics.OpenGL;
using OpenTK;
using JSim.Core.Render;
using System.Runtime.InteropServices;

namespace JSim.OpenTK
{
    /// <summary>
    /// Struct representing an OpenGL VBO.
    /// </summary>
    /// <remarks>
    /// VBO's are used to store vertex data on the GPU.
    /// Contains the vbo and element buffer locations and element size.
    /// </remarks>
    public struct Vbo { public int VboID, EboID, NumElements; }

    /// <summary>
    /// Static class to provide static VBO helper functions to aid building and drawing VBO's.
    /// </summary>
    internal static class VboUtils
    {
        /// <summary>
        /// Constructs a VBO, allocates it space on the GPU and then uploads the data.
        /// </summary>
        /// <param name="vertices">List of vertics to construct.</param>
        /// <param name="indices">Indices representing the drawing order for the vertices.</param>
        /// <returns>Vbo object containing the buffer pointers.</returns>
        public static Vbo CreateVbo(Vertex[] vertices, uint[] indices)
        {
            int size;
            Vbo handle = new Vbo();
            handle.NumElements = vertices.Length;
            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Marshal.SizeOf(typeof(Vertex))), vertices, BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (vertices.Length * Marshal.SizeOf(typeof(Vertex)) != size)
            {
                // TODO
                //Logger.Log("Error: Vertex data not uploaded correctly");
                throw new InvalidOperationException("Vertex data not uploaded correctly");
            }

            GL.GenBuffers(1, out handle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(uint)), indices, BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indices.Length * sizeof(uint) != size)
            {
                // TODO
                //Logger.Log("Error: Element data not uploaded correctly");
                throw new InvalidOperationException("Element data not uploaded correctly");
            }

            handle.NumElements = indices.Length;
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            return handle;
        }

        /// <summary>
        /// Deletes a vbo from gpu memory.
        /// </summary>
        /// <param name="handle">Vbo to delete.</param>
        public static void DeleteVbo(Vbo handle)
        {
            GL.DeleteBuffer(handle.VboID);
            GL.DeleteBuffer(handle.EboID);
        }

        /// <summary>
        /// Draws a VBO to the render canvas.
        /// </summary>
        /// <param name="handle">VBO to draw.</param>
        /// <param name="type">Type of OpenGL primitive to draw.</param>
        public static void Draw(Vbo handle, PrimitiveType type)
        {
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);

            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, new IntPtr(0));
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, new IntPtr(12));
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, new IntPtr(24));
            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, new IntPtr(40));

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.DrawElements(type, handle.NumElements, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.DisableVertexAttribArray(3);
        }
    }
}
