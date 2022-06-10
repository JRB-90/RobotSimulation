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

            var vertices1 =
                new Vertex[]
                {
                    new Vertex(0, new Vector3D(0.0, 0.0, 0.0)),
                    new Vertex(0, new Vector3D(1.0, 0.0, 0.0)),
                    new Vertex(0, new Vector3D(0.0, 1.0, 0.0)),
                };

            var indices1 =
                new ushort[]
                {
                    0, 1, 1, 2, 2, 0
                };

            vao1 = VAO.CreateVAO(glb, vertices1, indices1);

            var vertices2 =
                new Vertex[]
                {
                    new Vertex(0, new Vector3D(-0.3, 0.0, 0.0)),
                    new Vertex(0, new Vector3D(-0.3, -1.5, 0.0)),
                    new Vertex(0, new Vector3D(-0.6, -1.5, 0.0)),
                };

            var indices2 =
                new ushort[]
                {
                    0, 1, 2
                };

            vao2 = VAO.CreateVAO(glb, vertices2, indices2);

            sm = new ShaderManager(new AvaloniaLogger(), glb, new GLVersion(4, 0));

            CheckError();
        }

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            VAO.DeleteVAO(glb, vao1);

            CheckError();
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            SetDefaultOptions();

            glb.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            glb.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glb.Enable(GL_DEPTH_TEST);
            glb.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

            var camera = new StandardCamera((int)Bounds.Width, (int)Bounds.Height);
            camera.PositionInWorld = new Transform3D(4, 4, 4, 0, 0, 0);
            camera.LookAtPoint(Vector3D.Origin, Vector3D.UnitZ);

            var material1 = Material.FromSingleColor(new Color(1.0f, 0.0f, 0.0f, 1.0f));
            var material2 = Material.FromSingleColor(new Color(1.0f, 1.0f, 0.0f, 0.0f));

            CheckError();

            IShader shader = sm.FindShader(ShadingType.Solid);
            shader.Bind();

            CheckError();

            shader.UpdateUniforms(Transform3D.Identity, camera, material1, SceneLighting.Default);

            CheckError();

            VAO.DrawVAO(glb, vao1, GL_LINES);

            shader.UpdateUniforms(Transform3D.Identity, camera, material2, SceneLighting.Default);

            VAO.DrawVAO(glb, vao2, GL_TRIANGLES);

            CheckError();

            shader.Unbind();

            CheckError();
        }

        private void SetDefaultOptions()
        {
            glb.FrontFace(GL_CW);
            glb.CullFace(GL_BACK);
            glb.Enable(GL_CULL_FACE);
            glb.Enable(GL_DEPTH_TEST);
            glb.Enable(GL_DEPTH_CLAMP);
            //glb.Enable(EnableCap.Texture2D);

            glb.Enable(GL_POINT_SMOOTH);
            glb.Hint(GL_POINT_SMOOTH_HINT, GL_NICEST);
            glb.Enable(GL_LINE_SMOOTH);
            glb.Hint(GL_LINE_SMOOTH_HINT, GL_NICEST);
            //glb.Enable(EnableCap.PolygonSmooth);
            //glb.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            glb.PolygonMode(GL_FRONT, GL_FILL);

            glb.PointSize(5);
            glb.LineWidth(5);

            CheckError();

            ResetStencilBufferOptions();

            CheckError();
        }

        private void ResetStencilBufferOptions()
        {
            glb.Disable(GL_STENCIL_TEST);
            glb.ClearStencil(0xFF);
            glb.Clear(GL_STENCIL_BUFFER_BIT);
            glb.StencilFunc(GL_ALWAYS, 1, -1);
            glb.StencilOp(GL_KEEP, GL_KEEP, GL_REPLACE);
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

        private VAO vao1;
        private VAO vao2;
        private ShaderManager sm;
    }
}
