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
        }

        public OpenTKControlBase(
            ISharedGlContextFactory glContextFactory,
            OpenGlControlSettings settings)
          :
            base(UpdateSettings(settings, glContextFactory))
        {
            this.glContextFactory = glContextFactory;
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
