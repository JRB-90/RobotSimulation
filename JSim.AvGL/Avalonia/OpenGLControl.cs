using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using JSim.Core.Maths;
using JSim.Core.Render;
using System.Diagnostics;
using static Avalonia.OpenGL.GlConsts;

namespace JSim.AvGL
{
    public class OpenGLControl : OpenGlControlBase
    {
        protected unsafe override void OnOpenGlInit(GlInterface gl, int fb)
        {
            glb = new GLBindingsInterface(gl);

            CheckError();

            Trace.WriteLine($"Renderer: {glb.GetString(GL_RENDERER)} Version: {glb.GetString(GL_VERSION)}");

            var vertices =
                new Vertex[]
                {
                    new Vertex(0, new Vector3D(0.0, 0.0, 0.0)),
                    new Vertex(0, new Vector3D(1.0, 0.0, 0.0)),
                };

            var indices =
                new uint[]
                {
                    0, 1
                };

            vbo = VboUtils.CreateVbo(glb, vertices, indices);

            CheckError();
        }

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            VboUtils.DeleteVbo(glb, vbo);

            CheckError();
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            glb.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            glb.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glb.Enable(GL_DEPTH_TEST);
            glb.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

            //VboUtils.Draw(glb, vbo, GL_LINES);

            CheckError();
        }

        private void CheckError()
        {
            int err;
            while ((err = glb.GetError()) != GL_NO_ERROR)
            {
                Trace.WriteLine(err);
            }
        }

        private GLBindingsInterface glb;

        private Vbo vbo;
    }
}
