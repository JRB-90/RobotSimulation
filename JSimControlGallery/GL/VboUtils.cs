using Avalonia.OpenGL;
using JSim.Core.Render;
using System;
using System.Runtime.InteropServices;
using static Avalonia.OpenGL.GlConsts;

namespace JSimControlGallery.GL
{
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
        public static Vbo CreateVbo(
            GLBindingsInterface gl,
            Vertex[] vertices, 
            uint[] indices)
        {
            Vbo handle = new Vbo();
            handle.NumElements = vertices.Length;
            var buf = new int[] { 0 };
            gl.GenBuffers(1, buf);
            handle.VboID = buf[0];
            gl.BindBuffer(GL_ARRAY_BUFFER, handle.VboID);

            unsafe
            {
                fixed (Vertex* pVertices = vertices)
                {
                    gl.BufferData(
                        GL_ARRAY_BUFFER, 
                        (IntPtr)(vertices.Length * Marshal.SizeOf(typeof(Vertex))), 
                        new IntPtr((void*)pVertices), 
                        GL_STATIC_DRAW
                    );
                }
            }

            gl.GetBufferParameter(
                GL_ARRAY_BUFFER, 
                GL_BUFFER_SIZE, 
                out int size
            );

            if (vertices.Length * Marshal.SizeOf(typeof(Vertex)) != size)
            {
                throw new InvalidOperationException("Vertex data not uploaded correctly");
            }

            buf[0] = 0;
            gl.GenBuffers(1, buf);
            handle.EboID = buf[0];
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, handle.EboID);

            unsafe
            {
                fixed (uint* pIndices = indices)
                {
                    gl.BufferData(
                        GL_ELEMENT_ARRAY_BUFFER, 
                        (IntPtr)(indices.Length * sizeof(uint)), 
                        new IntPtr((void*)pIndices), 
                        GL_STATIC_DRAW
                    );
                }
            }

            gl.GetBufferParameter(
                GL_ELEMENT_ARRAY_BUFFER, 
                GL_BUFFER_SIZE, 
                out size
            );

            if (indices.Length * sizeof(uint) != size)
            {
                throw new InvalidOperationException("Element data not uploaded correctly");
            }

            handle.NumElements = indices.Length;
            gl.BindBuffer(GL_ARRAY_BUFFER, 0);
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

            return handle;
        }

        /// <summary>
        /// Deletes a vbo from gpu memory.
        /// </summary>
        /// <param name="handle">Vbo to delete.</param>
        public static void DeleteVbo(
            GLBindingsInterface gl,
            Vbo handle)
        {
            gl.DeleteBuffers(2, new int[] { handle.VboID, handle.EboID });
            handle.VboID = 0;
            handle.EboID = 0;
        }

        /// <summary>
        /// Draws a VBO to the render canvas.
        /// </summary>
        /// <param name="handle">VBO to draw.</param>
        /// <param name="mode">Type of OpenGL primitive to draw.</param>
        public static void Draw(
            GLBindingsInterface gl,
            Vbo handle, 
            int mode)
        {
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);
            gl.EnableVertexAttribArray(2);
            gl.EnableVertexAttribArray(3);

            gl.BindBuffer(GL_ARRAY_BUFFER, handle.VboID);
            gl.VertexAttribPointer(0, 3, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(0));
            gl.VertexAttribPointer(1, 3, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(12));
            gl.VertexAttribPointer(2, 4, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(24));
            gl.VertexAttribPointer(3, 2, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(40));

            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, handle.EboID);
            gl.DrawElements(mode, handle.NumElements, GL_UNSIGNED_INT, IntPtr.Zero);

            gl.DisableVertexAttribArray(0);
            gl.DisableVertexAttribArray(1);
            gl.DisableVertexAttribArray(2);
            gl.DisableVertexAttribArray(3);
        }
    }
}
