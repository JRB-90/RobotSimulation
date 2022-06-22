using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using JSim.Core.Input;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.SceneGraph;

namespace JSim.AvGL
{
    public class OpenGLControl : OpenGlControlBase, IRenderingSurface
    {
        readonly IRenderingEngine renderingEngine;

        public OpenGLControl(IRenderingEngine renderingEngine)
        {
            this.renderingEngine = renderingEngine;

            RenderTransform =
                new TransformGroup()
                {
                    Children = new Transforms()
                    {
                        new ScaleTransform(1.0, -1.0)
                    }
                };

            camera =
                new StandardCamera(
                    (int)Bounds.Width,
                    (int)Bounds.Height
                );

            camera.CameraController =
                new MouseOrbitController(
                    new MouseInputProvider(this)
                );

            camera.PositionInWorld = new Transform3D(4, 4, 4, 0, 0, 0);
            camera.LookAtPoint(Vector3D.Origin, Vector3D.UnitZ);
            camera.CameraModified += OnCameraModified;

            SceneLighting = SceneLighting.Default;

            EffectiveViewportChanged += OpenGLControl_EffectiveViewportChanged;
        }

        public static readonly StyledProperty<IBrush> ClearColorProperty =
            AvaloniaProperty.Register<OpenGLControl, IBrush>(
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

        public ICamera? Camera
        {
            get => camera;
            set
            {
                camera = value;
                if (camera != null)
                {
                    camera.CameraModified += OnCameraModified;
                }
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
                    if (scene != null)
                    {
                        scene.SceneTreeModified += OnSceneTreeModified; ;
                    }
                }
                RequestRender();
            }
        }

        public SceneLighting SceneLighting { get; }

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

        protected unsafe override void OnOpenGlInit(GlInterface gl, int fb)
        {
            glb = new GLBindingsInterface(gl);

            GLUtils.CheckError(glb);
        }

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            renderingEngine?.Dispose();
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            Camera?.Update(this);
            renderingEngine?.Render(this, Scene);
        }

        private void OpenGLControl_EffectiveViewportChanged(object? sender, EffectiveViewportChangedEventArgs e)
        {
            RequestRender();
        }

        private void OnCameraModified(object sender, CameraModifiedEventArgs e)
        {
            RequestRender();
        }

        private void OnSceneTreeModified(object sender, SceneTreeModifiedEventArgs e)
        {
            RequestRender();
        }

        private GLBindingsInterface? glb;
        private ICamera? camera;
        private IScene? scene;

        private static object sceneLock = new object();
    }
}
