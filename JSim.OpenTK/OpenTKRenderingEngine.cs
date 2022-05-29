using Avalonia.Media;
using JSim.Core;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace JSim.OpenTK
{
    /// <summary>
    /// Provides a rendering engine that uses OpenTK to call OpenGL functions
    /// and render a scene to a surface.
    /// </summary>
    public class OpenTKRenderingEngine : IRenderingEngine
    {
        public const int MAX_LIGHTS = 8;
        const float DEFAULT_POINT_SIZE = 5.0f;
        const float DEFAULT_LINE_WIDTH = 0.1f;

        readonly ILogger logger;
        readonly IGlContextManager glContextManager;

        public OpenTKRenderingEngine(
            ILogger logger,
            IGlContextManager glContextManager)
        {
            this.glContextManager = glContextManager;

            glContextManager.RunOnResourceContext(
                () =>
                {
                    GL.GetInteger(GetPName.MajorVersion, out int glMajor);
                    GL.GetInteger(GetPName.MinorVersion, out int glMinor);
                    gLVersion = new GLVersion(glMajor, glMinor);
                    
                    shaderManager = 
                        new ShaderManager(
                            logger,
                            gLVersion
                        );
                }
            );
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
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

            if (surface is OpenTKControl openTKSurface)
            {
                RenderScene(openTKSurface, scene);
            }
            else
            {
                throw new InvalidOperationException("Can only render to OpenTKControl rendering surface");
            }

            sw.Stop();
            var elapsedNS = (double)sw.ElapsedTicks / ((double)TimeSpan.TicksPerMillisecond / 1000.0);
            //Trace.WriteLine($"Render time {elapsedNS / 1000.0:F3}ms", "Debug");
        }

        /// <summary>
        /// Renders a test scene to the given surface.
        /// </summary>
        internal void RenderTestScene(IRenderingSurface surface)
        {
            GL.Viewport(0, 0, surface.SurfaceWidth, surface.SurfaceHeight);

            var hue = (float)_stopwatch.Elapsed.TotalSeconds * 0.15f % 1;
            var c = Color4.FromHsv(new Vector4(hue, 0.75f, 0.75f, 1));
            GL.ClearColor(c);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(Color4.Red);
            GL.Vertex2(0.0f, 0.5f);

            GL.Color4(Color4.Green);
            GL.Vertex2(0.58f, -0.5f);

            GL.Color4(Color4.Blue);
            GL.Vertex2(-0.58f, -0.5f);

            GL.End();
        }

        private void RenderScene(
            OpenTKControl surface,
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

            GL.Flush();
        }

        private void RenderSceneAssembly(
            OpenTKControl surface,
            ISceneAssembly assembly)
        {
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
            OpenTKControl surface,
            ISceneEntity entity)
        {
            RenderGeometryRecursive(
                surface,
                entity.GeometryContainer.Root
            );
        }

        private void RenderGeometryRecursive(
            OpenTKControl surface,
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
            OpenTKControl surface,
            IGeometry geometry)
        {
            if (!geometry.IsVisible ||
                shaderManager == null)
            {
                return;
            }

            if (geometry is OpenTKGeometry tkGeometry &&
                surface.Camera != null)
            {

                IShader shader = shaderManager.FindShader(tkGeometry.Material.Shading);
                shader.Bind();

                shader.UpdateUniforms(
                    tkGeometry.WorldFrame,
                    surface.Camera,
                    tkGeometry.Material,
                    surface.SceneLighting
                );

                VboUtils.Draw(
                    tkGeometry.VBO,
                    ToPrimitiveType(tkGeometry.GeometryType)
                );

                shader.Unbind();
            }
        }

        private void SetDefaultOptions()
        {
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DepthClamp);
            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            //GL.Enable(EnableCap.PolygonSmooth);
            //GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

            GL.PointSize(DEFAULT_POINT_SIZE);
            GL.LineWidth(DEFAULT_LINE_WIDTH);

            ResetStencilBufferOptions();
        }

        private void ResetStencilBufferOptions()
        {
            GL.Disable(EnableCap.StencilTest);
            GL.ClearStencil(0xFF);
            GL.Clear(ClearBufferMask.StencilBufferBit);
            GL.StencilFunc(StencilFunction.Always, 1, -1);
            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
        }

        private void SetViewport(OpenTKControl surface)
        {
            GL.Viewport(
                0,
                0,
                surface.SurfaceWidth,
                surface.SurfaceHeight
            );
        }

        private void ClearScreen(OpenTKControl surface)
        {
            if (surface.ClearColor is ISolidColorBrush solidColorBrush)
            {
                GL.ClearColor(
                    new Color4(
                        solidColorBrush.Color.R,
                        solidColorBrush.Color.G,
                        solidColorBrush.Color.B,
                        solidColorBrush.Color.A
                    )
                );
            }
            else
            {
                GL.ClearColor(Color4.Black);
            }

            GL.Clear(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit |
                ClearBufferMask.StencilBufferBit
            );
        }

        private PrimitiveType ToPrimitiveType(GeometryType geometryType)
        {
            switch (geometryType)
            {
                case GeometryType.Points:
                    return PrimitiveType.Points;
                case GeometryType.Wireframe:
                    return PrimitiveType.Lines;
                case GeometryType.Solid:
                    return PrimitiveType.Triangles;
                default:
                    return PrimitiveType.Points;
            }
        }

        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private ShaderManager? shaderManager;
        private GLVersion gLVersion;
    }
}
