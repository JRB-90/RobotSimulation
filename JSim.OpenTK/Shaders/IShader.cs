namespace JSim.OpenTK
{
    /// <summary>
    /// Interface defining the behavior of all opengl shaders.
    /// </summary>
    internal interface IShader : IDisposable
    {
        /// <summary>
        /// Binds a shader to the current context.
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbinds a shader from the current context.
        /// </summary>
        void Unbind();
    }
}
