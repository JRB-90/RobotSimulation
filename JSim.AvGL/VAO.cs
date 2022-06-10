using JSim.Core.Render;
using System.Runtime.InteropServices;
using static Avalonia.OpenGL.GlConsts;

namespace JSim.AvGL
{
    public class VAO
    {
        public int Handle;
        public int ElementCount;

        public static VAO CreateVAO(
            GLBindingsInterface gl,
            Vertex[] vertices,
            ushort[] indices)
        {
            VAO vao = new VAO();
            vao.ElementCount = indices.Length;
            vao.Handle = gl.GenVertexArray();
            gl.BindVertexArray(vao.Handle);

            int vbo = CreateVBO(gl, vertices);
            int ebo = CreateEBO(gl, indices);

            gl.EnableVertexAttribArray(0);
            gl.VertexAttribPointer(0, 3, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(0));
            gl.EnableVertexAttribArray(1);
            gl.VertexAttribPointer(1, 3, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(12));
            gl.EnableVertexAttribArray(2);
            gl.VertexAttribPointer(2, 4, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(24));
            gl.EnableVertexAttribArray(3);
            gl.VertexAttribPointer(3, 2, GL_FLOAT, 0, Vertex.SizeInBytes, new IntPtr(40));

            gl.BindVertexArray(0);
            gl.BindBuffer(GL_ARRAY_BUFFER, 0);
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

            return vao;
        }

        public static void DeleteVAO(
            GLBindingsInterface gl,
            VAO vao)
        {
            // TODO
        }

        public static int CreateVBO(
            GLBindingsInterface gl,
            Vertex[] vertices)
        {
            var buf = new int[] { 0 };
            gl.GenBuffers(1, buf);
            var vbo = buf[0];
            gl.BindBuffer(GL_ARRAY_BUFFER, vbo);

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

            return vbo;
        }

        public static int CreateEBO(
            GLBindingsInterface gl,
            ushort[] indices)
        {
            var buf = new int[] { 0 };
            gl.GenBuffers(1, buf);
            var ebo = buf[0];
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);

            unsafe
            {
                fixed (ushort* pIndices = indices)
                {
                    gl.BufferData(
                        GL_ELEMENT_ARRAY_BUFFER,
                        (IntPtr)(indices.Length * sizeof(ushort)),
                        new IntPtr((void*)pIndices),
                        GL_STATIC_DRAW
                    );
                }
            }

            gl.GetBufferParameter(
                GL_ELEMENT_ARRAY_BUFFER,
                GL_BUFFER_SIZE,
                out int size
            );

            if (indices.Length * sizeof(ushort) != size)
            {
                throw new InvalidOperationException("Element data not uploaded correctly");
            }

            return ebo;
        }

        public static void DrawVAO(
            GLBindingsInterface gl,
            VAO vao,
            int primitiveType)
        {
            gl.BindVertexArray(vao.Handle);
            gl.DrawElements(primitiveType, vao.ElementCount, GL_UNSIGNED_SHORT, IntPtr.Zero);
            gl.BindVertexArray(0);
        }
    }
}
