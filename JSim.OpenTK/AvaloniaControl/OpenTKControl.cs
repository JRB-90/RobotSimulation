using Avalonia;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using JSim.Core.Display;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    public class OpenTKControl : OpenTKControlBase, IRenderingSurface
    {
        public OpenTKControl(ISharedGlContextFactory glContextFactory)
          :
            base(
                glContextFactory,
                new OpenGlControlSettings() { ContinuouslyRender = false }
            )
        {
            camera =
                new StandardCamera(
                    (int)Bounds.Width,
                    (int)Bounds.Height
                );

            camera.CameraModified += OnCameraModified;
            EffectiveViewportChanged += OpenTKControl_EffectiveViewportChanged;
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

        public void RequestRender()
        {
            FireRequestRenderEvent();
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

        private void FireRequestRenderEvent()
        {
            RenderRequested?.Invoke(this, new RenderRequestedEventArgs());
        }

        private void OnCameraModified(object sender, CameraModifiedEventArgs e)
        {
            FireRequestRenderEvent();
        }

        private void OpenTKControl_EffectiveViewportChanged(object? sender, global::Avalonia.Layout.EffectiveViewportChangedEventArgs e)
        {
            Camera.Update(this);
            FireRequestRenderEvent();
        }

        private ICamera camera;
    }
}
