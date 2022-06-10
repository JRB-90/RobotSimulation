using Avalonia.Media;
using JSim.Core;
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
        readonly GLBindingsInterface gl;

        public OpenGLRenderingEngine(
            ILogger logger,
            GLBindingsInterface gl)
        {
            this.logger = logger;
            this.gl = gl;
        }

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

            //if (surface is OpenTKControl openTKSurface)
            //{
            //    RenderScene(openTKSurface, scene);
            //}
            //else
            //{
            //    throw new InvalidOperationException("Can only render to OpenTKControl rendering surface");
            //}

            sw.Stop();
            var elapsedNS = (double)sw.ElapsedTicks / ((double)TimeSpan.TicksPerMillisecond / 1000.0);
            //Trace.WriteLine($"Render time {elapsedNS / 1000.0:F3}ms", "Debug");
        }

        //private void SetViewport(OpenGLControl surface)
        //{
        //    gl.Viewport(
        //        0,
        //        0,
        //        surface.SurfaceWidth,
        //        surface.SurfaceHeight
        //    );
        //}

        private void SetDefaultOptions()
        {
            gl.FrontFace(GL_CW);
            gl.CullFace(GL_BACK);
            gl.Enable(GL_CULL_FACE);
            gl.Enable(GL_DEPTH_TEST);
            gl.Enable(GL_DEPTH_CLAMP);
            //gl.Enable(EnableCap.Texture2D);

            gl.Enable(GL_POINT_SMOOTH);
            gl.Hint(GL_POINT_SMOOTH_HINT, GL_NICEST);
            gl.Enable(GL_LINE_SMOOTH);
            gl.Hint(GL_LINE_SMOOTH_HINT, GL_NICEST);
            //gl.Enable(GL_POLYGON_SMOOTH);
            //gl.Hint(GL_POLYGON_SMOOTH_HINT, GL_NICEST);
            gl.PolygonMode(GL_FRONT, GL_FILL);

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

        //private void ClearScreen(OpenGLControl surface)
        //{
        //    if (surface.ClearColor is ISolidColorBrush solidColorBrush)
        //    {
        //        gl.ClearColor(
        //            solidColorBrush.Color.R.ArgbByteToFloat(),
        //            solidColorBrush.Color.G.ArgbByteToFloat(),
        //            solidColorBrush.Color.B.ArgbByteToFloat(),
        //            solidColorBrush.Color.A.ArgbByteToFloat()
        //        );
        //    }
        //    else
        //    {
        //        gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        //    }

        //    gl.Clear(
        //        GL_COLOR_BUFFER_BIT |
        //        GL_DEPTH_BUFFER_BIT |
        //        GL_STENCIL_BUFFER_BIT
        //    );
        //}

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
    }
}
