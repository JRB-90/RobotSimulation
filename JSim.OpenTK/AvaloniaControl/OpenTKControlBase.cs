using Avalonia.Media;
using Avalonia.OpenGL.Controls;
using Avalonia.OpenTK;

namespace JSim.OpenTK
{
    public abstract class OpenTKControlBase : ExtendedOpenGlControlBase
    {
        readonly ISharedGlContextFactory glContextFactory;

        public OpenTKControlBase(ISharedGlContextFactory glContextFactory)
          : 
            this(
                glContextFactory,
                new OpenGlControlSettings()
            )
        {
            RenderTransform =
                new TransformGroup()
                {
                    Children = new Transforms() { new ScaleTransform(1.0, -1.0) }
                };
        }

        public OpenTKControlBase(
            ISharedGlContextFactory glContextFactory,
            OpenGlControlSettings settings)
          :
            base(UpdateSettings(settings, glContextFactory))
        {
            this.glContextFactory = glContextFactory;

            RenderTransform =
                new TransformGroup()
                {
                    Children = new Transforms() { new ScaleTransform(1.0, -1.0) }
                };
        }

        private static OpenGlControlSettings UpdateSettings(
            OpenGlControlSettings settings,
            ISharedGlContextFactory glContextFactory)
        {
            settings = settings.Clone();
            if (settings.Context == null && settings.ContextFactory == null)
            {
                settings.ContextFactory = () => glContextFactory.CreateSharedContext();
            }

            return settings;
        }
    }
}
