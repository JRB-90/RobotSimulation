using JSim.Core.Maths;

namespace JSim.Core.Input
{
    /// <summary>
    /// Defines the bahaviour of all camera controllers.
    /// Camera controllers are used to manipulate a camera in response to inputs.
    /// </summary>
    public interface ICameraController
    {
        /// <summary>
        /// Gets the controlled position in world coordinates of the camera.
        /// </summary>
        Transform3D CameraPosition { get; }

        /// <summary>
        /// Flag to designate if the controller is activated or not.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Event fired when a new camera position has been calculated.
        /// </summary>
        event NewPositionCalculatedEventHandler? NewPositionCalculated;
    }
}
