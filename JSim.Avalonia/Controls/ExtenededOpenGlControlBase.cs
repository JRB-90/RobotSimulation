using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.OpenGL.Imaging;
using Avalonia.Threading;
using static Avalonia.OpenGL.GlConsts;

namespace JSim.Avalonia.Controls
{
    public abstract class ExtendedOpenGlControlBase : Control
    {
        readonly OpenGlControlSettings settings;

        public ExtendedOpenGlControlBase(OpenGlControlSettings settings)
        {
            this.settings = settings.Clone();
        }

        public ExtendedOpenGlControlBase()
          :
            this(new OpenGlControlSettings())
        {
        }

        protected GlVersion GlVersion { get; private set; }

        public sealed override void Render(DrawingContext drawingContext)
        {
            if (!EnsureInitialized() ||
                context == null ||
                attachment == null ||
                bitmap == null)
            {
                return;
            }

            using (context.MakeCurrent())
            {
                context.GlInterface.BindFramebuffer(GL_FRAMEBUFFER, fb);
                EnsureTextureAttachment();
                EnsureDepthBufferAttachment(context.GlInterface);

                if (!CheckFramebufferStatus(context.GlInterface))
                {
                    return;
                }

                OnOpenGlRender(context.GlInterface, fb);

                attachment.Present();
            }

            drawingContext.DrawImage(
                bitmap, 
                new Rect(bitmap.Size), 
                Bounds
            );

            base.Render(drawingContext);

            if (settings.ContinuouslyRender)
            {
                Dispatcher.UIThread.Post(
                    InvalidateVisual, 
                    DispatcherPriority.Background
                );
            }
        }

        private void CheckError(GlInterface gl)
        {
            int err;
            while ((err = gl.GetError()) != GL_NO_ERROR)
            {
                Console.WriteLine(err);
            }
        }

        void EnsureTextureAttachment()
        {
            if (context == null)
            {
                return;
            }

            context.GlInterface.BindFramebuffer(GL_FRAMEBUFFER, fb);

            if (bitmap == null || 
                attachment == null || 
                bitmap.PixelSize != GetPixelSize())
            {
                attachment?.Dispose();
                attachment = null;
                bitmap?.Dispose();
                bitmap = null;

                bitmap = 
                    new OpenGlBitmap(
                        GetPixelSize(), 
                        new Vector(96, 96)
                    );

                attachment = bitmap.CreateFramebufferAttachment(context);
            }
        }

        void EnsureDepthBufferAttachment(GlInterface gl)
        {
            var size = GetPixelSize();
            if (size == depthBufferSize && depthBuffer != 0)
            {
                return;
            }

            gl.GetIntegerv(GL_RENDERBUFFER_BINDING, out var oldRenderBuffer);
            if (depthBuffer != 0)
            {
                gl.DeleteRenderbuffers(1, new[] { depthBuffer });
            }

            var oneArr = new int[1];
            gl.GenRenderbuffers(1, oneArr);
            depthBuffer = oneArr[0];

            gl.BindRenderbuffer(
                GL_RENDERBUFFER, 
                depthBuffer
            );

            gl.RenderbufferStorage(
                GL_RENDERBUFFER,
                GlVersion.Type == GlProfileType.OpenGLES ? GL_DEPTH_COMPONENT16 : GL_DEPTH_COMPONENT,
                size.Width, 
                size.Height
            );

            gl.FramebufferRenderbuffer(
                GL_FRAMEBUFFER, 
                GL_DEPTH_ATTACHMENT, 
                GL_RENDERBUFFER, 
                depthBuffer
            );

            gl.BindRenderbuffer(
                GL_RENDERBUFFER, 
                oldRenderBuffer
            );
        }

        void DisposeContextIfNeeded()
        {
            if (settings.Context == null)
            {
                context?.Dispose();
            }

            context = null;
        }

        public void Cleanup()
        {
            if (context != null)
            {
                using (context.MakeCurrent())
                {
                    var gl = context.GlInterface;
                    gl.BindTexture(GL_TEXTURE_2D, 0);
                    gl.BindFramebuffer(GL_FRAMEBUFFER, 0);
                    gl.DeleteFramebuffers(1, new[] { fb });
                    gl.DeleteRenderbuffers(1, new[] { depthBuffer });

                    attachment?.Dispose();
                    attachment = null;
                    bitmap?.Dispose();
                    bitmap = null;

                    try
                    {
                        if (isInitialized)
                        {
                            isInitialized = false;
                            OnOpenGlDeinit(context.GlInterface, fb);
                        }
                    }
                    finally
                    {
                        DisposeContextIfNeeded();
                    }
                }
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            Cleanup();
            base.OnDetachedFromVisualTree(e);
        }

        protected virtual void OnOpenGlInit(GlInterface gl, int fb)
        {
        }

        protected virtual void OnOpenGlDeinit(GlInterface gl, int fb)
        {
        }

        protected abstract void OnOpenGlRender(GlInterface gl, int fb);

        private bool EnsureInitializedCore()
        {
            if (context != null)
            {
                return true;
            }

            if (hasGlFailed)
            {
                return false;
            }

            var feature = AvaloniaLocator.Current.GetService<IPlatformOpenGlInterface>();

            if (feature == null)
            {
                return false;
            }

            try
            {
                if (settings.Context != null)
                {
                    context = settings.Context;
                }
                else if (settings.ContextFactory != null)
                {
                    context = settings.ContextFactory();

                    if (context == null)
                    {
                        throw new InvalidOperationException("Custom OpenGL context factory returned null");
                    }
                }
                else if (feature.CanShareContexts)
                {
                    context = feature.CreateSharedContext();
                }
                else
                {
                    context = 
                        feature.CreateOSTextureSharingCompatibleContext(
                            null, 
                            new List<GlVersion>
                            {
                                new GlVersion(GlProfileType.OpenGL, 3, 2, true),
                                new GlVersion(GlProfileType.OpenGLES, 3, 0),
                                new GlVersion(GlProfileType.OpenGLES, 2, 0),
                                new GlVersion(GlProfileType.OpenGL, 2, 0)
                            }
                        );
                }
            }
            catch (Exception e)
            {
                Logger.TryGet(LogEventLevel.Error,"OpenGL"
                    )?.Log(
                        "OpenGlControlBase",
                        "Unable to initialize OpenGL: unable to create additional OpenGL context: {exception}", 
                        e
                    );
                
                return false;
            }

            GlVersion = context.Version;

            try
            {
                bitmap = 
                    new OpenGlBitmap(
                        GetPixelSize(), 
                        new Vector(96, 96)
                    );

                if (!bitmap.SupportsContext(context))
                {
                    Logger.TryGet(LogEventLevel.Error, "OpenGL")
                        ?.Log(
                            "OpenGlControlBase",
                            "Unable to initialize OpenGL: unable to create OpenGlBitmap: OpenGL context is not compatible"
                        );

                    return false;
                }
            }
            catch (Exception e)
            {
                DisposeContextIfNeeded();

                Logger.TryGet(LogEventLevel.Error, "OpenGL")
                    ?.Log(
                        "OpenGlControlBase",
                        "Unable to initialize OpenGL: unable to create OpenGlBitmap: {exception}",
                        e
                    );
                
                return false;
            }

            using (context.MakeCurrent())
            {
                try
                {
                    depthBufferSize = GetPixelSize();
                    var gl = context.GlInterface;
                    var oneArr = new int[1];
                    gl.GenFramebuffers(1, oneArr);
                    fb = oneArr[0];
                    gl.BindFramebuffer(GL_FRAMEBUFFER, fb);

                    EnsureDepthBufferAttachment(gl);
                    EnsureTextureAttachment();

                    return CheckFramebufferStatus(gl);
                }
                catch (Exception e)
                {
                    Logger.TryGet(LogEventLevel.Error, "OpenGL")
                        ?.Log(
                            "OpenGlControlBase",
                            "Unable to initialize OpenGL FBO: {exception}",
                            e
                        );

                    return false;
                }
            }
        }

        private bool CheckFramebufferStatus(GlInterface gl)
        {
            var status = gl.CheckFramebufferStatus(GL_FRAMEBUFFER);

            if (status != GL_FRAMEBUFFER_COMPLETE)
            {
                int code;
                int lastError = 0;

                while ((code = gl.GetError()) != 0)
                {
                    if (lastError == code)
                    {
                        continue;
                    }
                    else
                    {
                        lastError = code;
                    }

                    Logger.TryGet(LogEventLevel.Error, "OpenGL")
                        ?.Log(
                            "OpenGlControlBase",
                            "Unable to initialize OpenGL FBO: {code}",
                            code
                        );
                }

                return false;
            }

            return true;
        }

        private bool EnsureInitialized()
        {
            if (isInitialized)
            {
                return true;
            }

            hasGlFailed = !(isInitialized = EnsureInitializedCore());

            if (hasGlFailed ||
                context == null)
            {
                return false;
            }

            using (context.MakeCurrent())
            {
                OnOpenGlInit(context.GlInterface, fb);
            }

            return true;
        }

        private PixelSize GetPixelSize()
        {
            if (VisualRoot == null)
            {
                throw new InvalidOperationException("Cannot get pixel size, visual root is null");
            }

            var scaling = VisualRoot.RenderScaling;

            return 
                new PixelSize(
                    Math.Max(1, (int)(Bounds.Width * scaling)),
                    Math.Max(1, (int)(Bounds.Height * scaling))
                );
        }

        private int fb;
        private int depthBuffer;
        private bool hasGlFailed;
        private bool isInitialized;
        private IGlContext? context;
        private OpenGlBitmap? bitmap;
        private IOpenGlBitmapAttachment? attachment;
        private PixelSize depthBufferSize;
    }
}
