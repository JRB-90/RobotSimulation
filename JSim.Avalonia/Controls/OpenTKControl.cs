using Avalonia;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace JSim.Avalonia.Controls
{
    public class OpenTKControl : OpenTKGlControl
    {
        public OpenTKControl()
          :
            base(new OpenGlControlSettings() { ContinuouslyRender = true })
        {
        }

        public static readonly StyledProperty<IBrush> ClearColorProperty =
            AvaloniaProperty.Register<OpenTKControl, IBrush>(
                nameof(ClearColor),
                Brushes.Black
            );

        public IBrush ClearColor
        {
            get { return GetValue(ClearColorProperty); }
            set { SetValue(ClearColorProperty, value); }
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            GL.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

            if (ClearColor is ISolidColorBrush solidColorBrush)
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
    }
}
