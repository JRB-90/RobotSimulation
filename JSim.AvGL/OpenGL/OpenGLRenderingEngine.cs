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
            var sw = new Stopwatch();
            sw.Start();

            if (surface is OpenGLControl openTKSurface)
            {
                RenderScene(openTKSurface, scene);
            }
            else
            {
                throw new InvalidOperationException("Can only render to OpenGLControl rendering surface");
            }

            sw.Stop();
            var elapsedNS = (double)sw.ElapsedTicks / ((double)TimeSpan.TicksPerMillisecond / 1000.0);
            //Trace.WriteLine($"Render time {elapsedNS / 1000.0:F3}ms", "Debug");
        }

        private void RenderScene(
            OpenGLControl surface,
            IScene? scene)
        {
            SetDefaultOptions();
            SetViewport(surface);
            ClearScreen(surface);

            if (surface.SceneLighting.Lights.Count > MAX_LIGHTS)
            {
                logger.Log($"Only {MAX_LIGHTS} lights supported", LogLevel.Error);
            }

            if (scene != null)
            {
                RenderSceneAssembly(
                    surface,
                    scene.Root
                );
            }

            gl.Flush();
        }

        private void RenderSceneAssembly(
            OpenGLControl surface,
            ISceneAssembly assembly)
        {
            if (!assembly.IsVisible)
            {
                return;
            }

            foreach (ISceneAssembly childAssembly in assembly.OfType<ISceneAssembly>())
            {
                RenderSceneAssembly(
                    surface,
                    childAssembly
                );
            }

            foreach (ISceneEntity entity in assembly.OfType<ISceneEntity>())
            {
                RenderSceneEntity(
                    surface,
                    entity
                );
            }
        }

        private void RenderSceneEntity(
            OpenGLControl surface,
            ISceneEntity entity)
        {
            if (!entity.IsVisible)
            {
                return;
            }

            RenderGeometryRecursive(
                surface,
                entity.GeometryContainer.Root
            );
        }

        private void RenderGeometryRecursive(
            OpenGLControl surface,
            IGeometry geometry)
        {
            foreach (IGeometry childGeometry in geometry.Children)
            {
                RenderGeometryRecursive(
                    surface,
                    childGeometry
                );
            }

            RenderGeometry(
                surface,
                geometry
            );
        }

        private void RenderGeometry(
            OpenGLControl surface,
            IGeometry geometry)
        {
            if (!geometry.IsVisible ||
                shaderManager == null)
            {
                return;
            }

            if (geometry is OpenGLGeometry glGeometry &&
                surface.Camera != null)
            {

                IShader shader = shaderManager.FindShader(glGeometry.Material.Shading);
                shader.Bind();

                shader.UpdateUniforms(
                    glGeometry.WorldFrame,
                    surface.Camera,
                    glGeometry.Material,
                    surface.SceneLighting
                );

                VAO.DrawVAO(
                    gl,
                    glGeometry.VAO,
                    ToPrimitiveType(glGeometry.GeometryType)
                );

                shader.Unbind();
            }
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
