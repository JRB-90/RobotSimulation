using Avalonia;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using JSim.Core.Display;
using JSim.Core.Render;
using JSim.Core.SceneGraph;

namespace JSim.OpenTK
{
    public class OpenTKControl : OpenTKControlBase, IRenderingSurface
    {
        readonly IRenderingEngine renderingEngine;

        public OpenTKControl(
            ISharedGlContextFactory glContextFactory,
            IRenderingEngine renderingEngine)
          :
            base(
                glContextFactory,
                new OpenGlControlSettings() { ContinuouslyRender = true }
            )
        {
            this.renderingEngine = renderingEngine;

            camera =
                new StandardCamera(
                    (int)Bounds.Width,
                    (int)Bounds.Height
                );

            camera.CameraModified += OnCameraModified;
            EffectiveViewportChanged += OpenTKControl_EffectiveViewportChanged;
            AttachedToVisualTree += OpenTKControl_AttachedToVisualTree;
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

        public IScene? Scene
        {
            get => scene;
            set
            {
                lock (sceneLock)
                {
                    scene = value;
                }
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

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            if (renderingEngine is OpenTKRenderingEngine tkRenderer)
            {
                lock (sceneLock)
                {
                    tkRenderer.Render(this, scene);
                }
            }
        }

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

        private void OpenTKControl_AttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
        {
            e.Root.Renderer.DrawFps = true;
        }

        private ICamera camera;
        private IScene? scene;

        private static object sceneLock = new object();
    }
}
