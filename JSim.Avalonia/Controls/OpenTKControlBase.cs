using Avalonia.OpenGL.Controls;
using Avalonia.OpenTK;

namespace JSim.Avalonia.Controls
{
    public abstract class OpenTKControlBase : ExtendedOpenGlControlBase
    {
        public OpenTKControlBase()
          : 
            this(new OpenGlControlSettings())
        {
        }

        public OpenTKControlBase(OpenGlControlSettings settings)
          :
            base(UpdateSettings(settings))
        {
        }

        private static OpenGlControlSettings UpdateSettings(OpenGlControlSettings settings)
        {
            settings = settings.Clone();
            if (settings.Context == null && settings.ContextFactory == null)
            {
                settings.ContextFactory = () => AvaloniaOpenTKIntegration.CreateCompatibleContext(null);
            }

            return settings;
        }
    }
}
