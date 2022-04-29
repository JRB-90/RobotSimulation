using Avalonia.Media;
using Avalonia.OpenGL;
using JSim.Core;
using JSim.Core.Display;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace JSim.OpenTK
{
    public class OpenTKRenderingEngine : IRenderingEngine
    {
        const float DEFAULT_POINT_SIZE = 5.0f;
        const float DEFAULT_LINE_WIDTH = 0.1f;

        readonly ILogger logger;
        readonly IGlContextManager glContextManager;

        public OpenTKRenderingEngine(
            ILogger logger,
            IGlContextManager glContextManager)
        {
            this.logger = logger;
            this.glContextManager = glContextManager;

            glContextManager.RunOnResourceContext(
                () => shaderManager = new ShaderManager(logger)
            );
        }

        public void Dispose()
        {
        }

        public void Render(
            IRenderingSurface surface, 
            IScene scene)
        {
            var sw = new Stopwatch();
            sw.Start();

            if (surface is OpenTKControl openTKSurface)
            {
                openTKSurface.Render(() => RenderScene(openTKSurface, scene));
                openTKSurface.InvalidateVisual();
            }
            else
            {
                throw new InvalidOperationException("Can only render to OpenTKControl rendering surface");
            }

            sw.Stop();
            logger.Log($"Render time {sw.ElapsedTicks}", LogLevel.Debug);
        }

        private void RenderScene(
            OpenTKControl surface,
            IScene scene)
        {
            SetDefaultOptions();
            SetViewport(surface);
            ClearScreen(surface);

            RenderSceneAssembly(
                surface,
                scene.Root
            );

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

            if (geometry is OpenTKGeometry tkGeometry)
            {

                IShader shader = shaderManager.FindShader(tkGeometry.Material.Shading);
                shader.Bind();

                shader.UpdateUniforms(
                    tkGeometry.WorldFrame,
                    surface.Camera,
                    tkGeometry.Material
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
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DepthClamp);
            GL.Enable(EnableCap.Texture2D);

            //GL.Enable(EnableCap.PointSmooth);
            //GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
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

        private ShaderManager? shaderManager;
    }
}
