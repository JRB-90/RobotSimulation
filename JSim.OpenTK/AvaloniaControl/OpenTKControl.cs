using Avalonia;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
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
                new OpenGlControlSettings() { ContinuouslyRender = false }
            )
        {
            this.renderingEngine = renderingEngine;

            ClearColor = new SolidColorBrush(new Avalonia.Media.Color(255, 32, 32, 56));

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
                RequestRender();
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
                RequestRender();
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
                RequestRender();
            }
        }

        public void Dispose()
        {
        }

        public void RequestRender()
        {
            Dispatcher.UIThread.Post(
                InvalidateVisual,
                DispatcherPriority.MaxValue
            );
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

        private void OnCameraModified(object sender, CameraModifiedEventArgs e)
        {
            RequestRender();
        }

        private void OpenTKControl_EffectiveViewportChanged(object? sender, global::Avalonia.Layout.EffectiveViewportChangedEventArgs e)
        {
            Camera.Update(this);
            RequestRender();
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
