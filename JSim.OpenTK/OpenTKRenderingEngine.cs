using Avalonia.Media;
using Avalonia.OpenGL;
using JSim.Core;
using JSim.Core.Display;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace JSim.OpenTK
{
    public class OpenTKRenderingEngine : IRenderingEngine
    {
        const float DEFAULT_POINT_SIZE = 10.0f;
        const float DEFAULT_LINE_WIDTH = 1.0f;

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
            if (surface is OpenTKControl openTKSurface)
            {
                openTKSurface.Render(() => RenderScene2(openTKSurface, scene));
                openTKSurface.InvalidateVisual();
            }
            else
            {
                throw new InvalidOperationException("Can only render to OpenTKControl rendering surface");
            }
        }

        private void RenderScene(
            OpenTKControl surface,
            IScene scene)
        {
            SetViewport(surface);
            ClearScreen(surface);

            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(Color4.Red);
            GL.Vertex2(0.0f, 0.5f);

            GL.Color4(Color4.Green);
            GL.Vertex2(0.58f, -0.5f);

            GL.Color4(Color4.Blue);
            GL.Vertex2(-0.58f, -0.5f);

            GL.End();
            GL.Flush();
        }

        private void RenderScene2(
            OpenTKControl surface,
            IScene scene)
        {
            SetDefaultOptions();
            SetViewport(surface);
            ClearScreen(surface);

            if (scene.TryFindByName("Entity", out ISceneObject? entity))
            {
                if (entity is ISceneEntity sceneEntity &&
                    sceneEntity.GeometryContainer.Root.Children.FirstOrDefault() is OpenTKGeometry geometry)
                {
                    IShader shader = shaderManager.FindShader(geometry.Material.Shading);
                    shader.Bind();

                    shader.UpdateUniforms(
                        Transform3D.Identity,
                        surface.Camera,
                        geometry.Material
                    );

                    VboUtils.Draw(
                        geometry.VBO,
                        PrimitiveType.Points
                    );

                    shader.Unbind();
                }
            }

            GL.Flush();
        }

        private void SetDefaultOptions()
        {
            //GL.FrontFace(FrontFaceDirection.Cw);
            //GL.CullFace(CullFaceMode.Back);
            //GL.Enable(EnableCap.CullFace);
            //GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.DepthClamp);
            //GL.Enable(EnableCap.Texture2D);

            //GL.Enable(EnableCap.PointSmooth);
            //GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            //GL.Enable(EnableCap.LineSmooth);
            //GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            //GL.Enable(EnableCap.PolygonSmooth);
            //GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            //GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PointSize(DEFAULT_POINT_SIZE);
            GL.LineWidth(DEFAULT_LINE_WIDTH);

            //ResetStencilBufferOptions();
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
                ClearBufferMask.DepthBufferBit
            );
        }

        private ShaderManager shaderManager;
    }
}
