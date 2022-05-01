using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define camera projection behavior.
    /// </summary>
    public interface ICameraProjection
    {
        /// <summary>
        /// Width in pixels of renderable area.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Height in pixels of renderable area.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Event fired when the projection parameters have been modified.
        /// </summary>
        public event ProjectionModifiedEventHandler? ProjectionModified;

        /// <summary>
        /// Gets the camera projection matrix.
        /// </summary>
        /// <returns>Projection matrix.</returns>
        Matrix<double> GetProjectionMatrix();
    }
}
