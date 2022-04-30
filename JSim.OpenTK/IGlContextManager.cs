namespace JSim.OpenTK
{
    /// <summary>
    /// Defines the behaviour of OpenGL context managers,
    /// which are designed to manage OpenGL contexts globally
    /// for an OpenGL application, providing access to a
    /// context for things such as resource creation etc.
    /// </summary>
    public interface IGlContextManager
    {
        /// <summary>
        /// Provides a mechanism for running an action on the
        /// resource creation context. This should be used when
        /// using OpenGL functions to create/destroy gpu resources.
        /// </summary>
        /// <param name="action">Action to be run on the context.</param>
        void RunOnResourceContext(Action action);
    }
}
