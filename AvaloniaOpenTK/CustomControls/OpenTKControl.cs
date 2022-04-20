using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace AvaloniaOpenTK.CustomControls
{
    public class OpenTKControl : OpenTKGlControl
    {
        public OpenTKControl()
          :
            base(new OpenGlControlSettings() { ContinuouslyRender = true })
        {
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            GL.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

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

        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    }
}
