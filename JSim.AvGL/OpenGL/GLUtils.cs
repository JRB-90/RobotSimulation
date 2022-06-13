using System.Diagnostics;
using static Avalonia.OpenGL.GlConsts;

namespace JSim.AvGL
{
    internal static class GLUtils
    {
        public static void CheckError(GLBindingsInterface gl)
        {
            int err;
            while ((err = gl.GetError()) != GL_NO_ERROR)
            {
                Trace.WriteLine("GL Error: " + ToErrorString(err));
            }
        }

        public static string ToErrorString(int errorCode)
        {
            switch (errorCode)
            {
                case GL_INVALID_ENUM:
                    return "Invalid Enum";

                case GL_INVALID_VALUE:
                    return "Invalid Value";

                case GL_INVALID_OPERATION:
                    return "Invalid Operation";

                case GL_INVALID_FRAMEBUFFER_OPERATION:
                    return "Invalid Operation";

                case GL_OUT_OF_MEMORY:
                    return "Out of Memory";

                default:
                    return "Unknown Error";
            }
        }
    }
}
