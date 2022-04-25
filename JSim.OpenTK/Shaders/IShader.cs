using JSim.Core.Maths;
using JSim.Core.Render;

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

        /// <summary>
        /// Updates all uniforms in the shader.
        /// </summary>
        /// <param name="model">Model matrix.</param>
        /// <param name="camera">Camera.</param>
        /// <param name="material">Material data.</param>
        void UpdateUniforms(
            Transform3D model,
            ICamera camera,
            IMaterial material
        );
    }
}
