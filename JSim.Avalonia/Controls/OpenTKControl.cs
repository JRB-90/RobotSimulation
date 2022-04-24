using Avalonia;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using JSim.Core.Display;
using JSim.Core.Render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace JSim.Avalonia.Controls
{
    public class OpenTKControl : OpenTKControlBase, IRenderingSurface
    {
        public OpenTKControl()
          :
            base(new OpenGlControlSettings() { ContinuouslyRender = false })
        {
            camera =
                new StandardCamera(
                    (int)Bounds.Width,
                    (int)Bounds.Height
                );

            camera.CameraModified += OnCameraModified;
        }

        public static readonly StyledProperty<IBrush> ClearColorProperty =
            AvaloniaProperty.Register<OpenTKControl, IBrush>(
                nameof(ClearColor),
                Brushes.Black
            );

        public IBrush ClearColor
        {
            get => GetValue(ClearColorProperty);
            set
            {
                SetValue(ClearColorProperty, value);
                FireRequestRenderEvent();
            }
        }

        public int SurfaceWidth => (int)Bounds.Width;

        public int SurfaceHeight => (int)Bounds.Height;

        public ICamera Camera
        {
            get => camera;
            set
            {
                camera = value;
                FireRequestRenderEvent();
            }
        }

        public event RenderRequestedEventHandler? RenderRequested;

        public void Dispose()
        {
        }

        public void Render(Action renderCallback)
        {
            this.renderCallback = renderCallback;
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            if (renderCallback != null)
            {
                renderCallback.Invoke();
                renderCallback = null;
            }
        }

        private Action? renderCallback;

        private void RenderTest()
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

        private void FireRequestRenderEvent()
        {
            RenderRequested?.Invoke(this, new RenderRequestedEventArgs());
        }

        private void OnCameraModified(object sender, CameraModifiedEventArgs e)
        {
            FireRequestRenderEvent();
        }

        private ICamera camera;
    }
}
