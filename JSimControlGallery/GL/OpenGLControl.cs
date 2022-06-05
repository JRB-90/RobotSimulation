using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using System.Diagnostics;
using static Avalonia.OpenGL.GlConsts;

namespace JSimControlGallery.GL
{
    public class OpenGLControl : OpenGlControlBase
    {
        protected unsafe override void OnOpenGlInit(GlInterface gl, int fb)
        {
            CheckError(gl);

            Trace.WriteLine($"Renderer: {gl.GetString(GL_RENDERER)} Version: {gl.GetString(GL_VERSION)}");
            glb = new GLBindingsInterface(gl);

            CheckError(gl);
        }

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            CheckError(gl);
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            gl.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            gl.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            gl.Enable(GL_DEPTH_TEST);
            gl.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

            CheckError(gl);
        }

        private void CheckError(GlInterface gl)
        {
            int err;
            while ((err = gl.GetError()) != GL_NO_ERROR)
            {
                Trace.WriteLine(err);
            }
        }

        private GLBindingsInterface glb;
    }
}
