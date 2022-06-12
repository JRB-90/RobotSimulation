using Avalonia.OpenGL;

namespace JSim.AvGL
{
    /// <summary>
    /// Provides a standard implementation of an OpenGL context manager.
    /// </summary>
    public class GlContextManager : IGlContextManager
    {
        readonly IGlContext rootContext;

        public GlContextManager(
            ISharedGlContextFactory glContextFactory)
        {
            rootContext = glContextFactory.CreateSharedContext();
        }

        /// <summary>
        /// Disposes of this manager and the resource context.
        /// </summary>
        public void Dispose()
        {
            rootContext.Dispose();
        }

        /// <summary>
        /// Provides a mechanism for running an action on the
        /// resource creation context. This should be used when
        /// using OpenGL functions to create/destroy gpu resources.
        /// </summary>
        /// <param name="action">Action to be run on the context.</param>
        public void RunOnResourceContext(Action<GlInterface> action)
        {
            using (rootContext.MakeCurrent())
            {
                action.Invoke(rootContext.GlInterface);
            }
        }
    }
}
