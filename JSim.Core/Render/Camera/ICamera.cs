using JSim.Core.Input;
using JSim.Core.Maths;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface defining the behaviour of cameras.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// The controller used for manipulating the camera.
        /// </summary>
        ICameraController? CameraController { get; set; }

        /// <summary>
        /// Gets and sets the position of the camera in the world.
        /// </summary>
        Transform3D PositionInWorld { get; set; }

        /// <summary>
        /// Gets and sets the camera projection.
        /// </summary>
        ICameraProjection CameraProjection { get; set; }

        /// <summary>
        /// Event fired when the camera parameters have changed.
        /// </summary>
        event CameraModifiedEventHandler? CameraModified;

        /// <summary>
        /// Gets the camera projection matrix.
        /// </summary>
        /// <returns>Camera projection matrix.</returns>
        Matrix<double> GetProjectionMatrix();

        /// <summary>
        /// Gets the camera view matrix.
        /// </summary>
        /// <returns>Camera view matrix.</returns>
        Matrix<double> GetViewMatrix();

        /// <summary>
        /// Updates the camera with information about the rendering surface.
        /// </summary>
        /// <param name="surface">Rendering surface to be used to update the camera.</param>
        void Update(IRenderingSurface surface);

        /// <summary>
        /// Rotates the camera to look at a given point.
        /// </summary>
        /// <param name="focusPoint">The point in space to focus the camera on.</param>
        /// <param name="up">The up vector of the world, in order to ensure the camera does not yaw.</param>
        void LookAtPoint(Vector3D focusPoint, Vector3D up);
    }
}
