using Avalonia.OpenGL;

namespace JSim.OpenTK
{
    public class GlContextManager : IGlContextManager
    {
        readonly ISharedGlContextFactory glContextFactory;
        readonly IGlContext rootContext;

        public GlContextManager(ISharedGlContextFactory glContextFactory)
        {
            this.glContextFactory = glContextFactory;
            rootContext = glContextFactory.CreateSharedContext();
        }

        public void Dispose()
        {
            rootContext.Dispose();
        }

        public void RunOnResourceContext(Action action)
        {
            using (rootContext.MakeCurrent())
            {
                action.Invoke();
            }
        }
    }
}
