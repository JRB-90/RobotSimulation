using Avalonia.Media;
using Avalonia.OpenGL;
using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using System.Diagnostics;
using static Avalonia.OpenGL.GlConsts;

namespace JSim.AvGL
{
    public class OpenGLRenderingEngine : IRenderingEngine
    {
        public const int MAX_LIGHTS = 8;
        const float DEFAULT_POINT_SIZE = 5.0f;
        const float DEFAULT_LINE_WIDTH = 0.1f;

        readonly ILogger logger;
        readonly IGlContextManager contextManager;

        public OpenGLRenderingEngine(
            ILogger logger,
            IGlContextManager contextManager)
        {
            this.logger = logger;
            this.contextManager = contextManager;

            contextManager.RunOnResourceContext((g) =>
            {
                gl = new GLBindingsInterface(g);

                Trace.WriteLine($"Renderer: {gl.GetString(GL_RENDERER)} Version: {gl.GetString(GL_VERSION)}");

                shaderManager = 
                    new ShaderManager(
                        logger, 
                        gl, 
                        new GLVersion(4, 0)
                    );

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

                vao1 = VAO.CreateVAO(gl, vertices1, indices1);

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

                vao2 = VAO.CreateVAO(gl, vertices2, indices2);

            });
        }

        public void Dispose()
        {
            shaderManager.Dispose();
            VAO.DeleteVAO(gl, vao1);
            VAO.DeleteVAO(gl, vao2);
        }

        /// <summary>
        /// Renders the given scene to the a surface.
        /// </summary>
        /// <param name="surface">Surface to render to.</param>
        /// <param name="scene">Scene to render to the surface.</param>
        /// <exception cref="InvalidOperationException">Thrown if surface is incompatible with this renderer.</exception>
        public void Render(
            IRenderingSurface surface,
            IScene? scene)
        {
            SetDefaultOptions();

            GLUtils.CheckError(gl);

            if (surface is OpenGLControl glSurface)
            {
                ClearScreen(glSurface);
                SetViewport(glSurface);
            }
            else
            {
                return;
            }

            var material1 = Material.FromSingleColor(new Core.Render.Color(1.0f, 0.0f, 0.0f, 1.0f));
            var material2 = Material.FromSingleColor(new Core.Render.Color(1.0f, 1.0f, 0.0f, 0.0f));

            if (surface.Camera == null)
            {
                return;
            }

            IShader shader = shaderManager.FindShader(ShadingType.Solid);
            shader.Bind();

            shader.UpdateUniforms(
                Transform3D.Identity, 
                surface.Camera, 
                material1, 
                surface.SceneLighting
            );

            VAO.DrawVAO(gl, vao1, GL_LINES);

            shader.UpdateUniforms(
                Transform3D.Identity,
                surface.Camera,
                material2,
                surface.SceneLighting
            );

            VAO.DrawVAO(gl, vao2, GL_TRIANGLES);

            shader.Unbind();
            gl.Flush();

            GLUtils.CheckError(gl);
        }

        private void SetViewport(OpenGLControl surface)
        {
            gl.Viewport(
                0,
                0,
                surface.SurfaceWidth,
                surface.SurfaceHeight
            );
        }

        private void SetDefaultOptions()
        {
            gl.FrontFace(GL_CW);
            gl.CullFace(GL_BACK);
            gl.Enable(GL_CULL_FACE);
            gl.Enable(GL_DEPTH_TEST);
            gl.Enable(GL_DEPTH_CLAMP);

            // TODO - Find right enum
            //gl.Enable(GL_TEXTURE_2D);

            //gl.Enable(GL_POINT_SMOOTH);
            //gl.Hint(GL_POINT_SMOOTH_HINT, GL_NICEST);
            //gl.Enable(GL_LINE_SMOOTH);
            //gl.Hint(GL_LINE_SMOOTH_HINT, GL_NICEST);
            //gl.Enable(GL_POLYGON_SMOOTH);
            //gl.Hint(GL_POLYGON_SMOOTH_HINT, GL_NICEST);
            //gl.PolygonMode(GL_FRONT, GL_FILL);

            gl.PointSize(DEFAULT_POINT_SIZE);
            gl.LineWidth(DEFAULT_LINE_WIDTH);

            ResetStencilBufferOptions();
        }

        private void ResetStencilBufferOptions()
        {
            gl.Disable(GL_STENCIL_TEST);
            gl.ClearStencil(0xFF);
            gl.Clear(GL_STENCIL_BUFFER_BIT);
            gl.StencilFunc(GL_ALWAYS, 1, -1);
            gl.StencilOp(GL_KEEP, GL_KEEP, GL_REPLACE);
        }

        private void ClearScreen(OpenGLControl surface)
        {
            if (surface.ClearColor is ISolidColorBrush solidColorBrush)
            {
                gl.ClearColor(
                    solidColorBrush.Color.R.ArgbByteToFloat(),
                    solidColorBrush.Color.G.ArgbByteToFloat(),
                    solidColorBrush.Color.B.ArgbByteToFloat(),
                    solidColorBrush.Color.A.ArgbByteToFloat()
                );
            }
            else
            {
                gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            }

            gl.Clear(
                GL_COLOR_BUFFER_BIT |
                GL_DEPTH_BUFFER_BIT |
                GL_STENCIL_BUFFER_BIT
            );
        }

        private int ToPrimitiveType(GeometryType geometryType)
        {
            switch (geometryType)
            {
                case GeometryType.Points:
                    return GL_POINTS;
                case GeometryType.Wireframe:
                    return GL_LINES;
                case GeometryType.Solid:
                    return GL_TRIANGLES;
                default:
                    return GL_POINTS;
            }
        }

        private VAO vao1;
        private VAO vao2;
        private GLBindingsInterface gl;
        private ShaderManager shaderManager;
    }
}
