using JSim.Core.Maths;

namespace JSim.Core.Input
{
    /// <summary>
    /// Interface to define the behaviour of all orbiting camera controllers.
    /// Orbit controllers allow a user to rotate the camera around a fixed point
    /// in the scene. This focus point can also be manipulated to allow for panning.
    /// </summary>
    public interface IOrbitCameraController : ICameraController
    {
        /// <summary>
        /// The focus point that the camera orbits around.
        /// </summary>
        Vector3D FocusPoint { get; set; }

        /// <summary>
        /// The direction of up in the 3D environment.
        /// </summary>
        Vector3D UpVector { get; set; }

        /// <summary>
        /// The speed at which orbit rotation moves.
        /// </summary>
        double RotationSpeed { get; set; }

        /// <summary>
        /// The speed at which zooming in and out occurs.
        /// </summary>
        double ZoomSpeed { get; set; }
    }
}
