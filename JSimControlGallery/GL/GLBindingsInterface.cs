using Avalonia.OpenGL;

namespace JSimControlGallery.GL
{
    internal class GLBindingsInterface : GlInterfaceBase<GlInterface.GlContextInfo>
    {
        public GLBindingsInterface(GlInterface gl) 
          : 
            base(
                gl.GetProcAddress,
                gl.ContextInfo)
        {
        }

        public delegate void GlDeleteVertexArrays(int count, int[] buffers);
        [GlMinVersionEntryPoint("glDeleteVertexArrays", 3, 0)]
        [GlExtensionEntryPoint("glDeleteVertexArraysOES", "GL_OES_vertex_array_object")]
        public GlDeleteVertexArrays DeleteVertexArrays { get; }

        public delegate void GlBindVertexArray(int array);
        [GlMinVersionEntryPoint("glBindVertexArray", 3, 0)]
        [GlExtensionEntryPoint("glBindVertexArrayOES", "GL_OES_vertex_array_object")]
        public GlBindVertexArray BindVertexArray { get; }
        public delegate void GlGenVertexArrays(int n, int[] rv);

        [GlMinVersionEntryPoint("glGenVertexArrays", 3, 0)]
        [GlExtensionEntryPoint("glGenVertexArraysOES", "GL_OES_vertex_array_object")]
        public GlGenVertexArrays GenVertexArrays { get; }

        public int GenVertexArray()
        {
            var rv = new int[1];
            GenVertexArrays(1, rv);

            return rv[0];
        }
    }
}
