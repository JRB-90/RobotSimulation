using Avalonia.Media;
using JSim.Avalonia.Controls;
using JSim.Core;
using JSim.Core.Display;
using JSim.Core.Render;
using JSim.Core.SceneGraph;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace JSim.OpenTK
{
    public class OpenTKRenderingEngine : IRenderingEngine
    {
        readonly ILogger logger;
        readonly ShaderManager shaderManager;

        public OpenTKRenderingEngine(ILogger logger)
        {
            this.logger = logger;
            shaderManager = new ShaderManager(logger);
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
                openTKSurface.Render(() => RenderScene(openTKSurface, scene));
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
            GL.Viewport(
                0,
                0, 
                surface.SurfaceWidth, 
                surface.SurfaceHeight
            );

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
    }
}
