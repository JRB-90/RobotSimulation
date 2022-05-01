using JSim.Core.Maths;

namespace JSim.Core.Input
{
    /// <summary>
    /// Base class for sharing functionality between camera controller implementations.
    /// </summary>
    public abstract class CameraControllerBase : ICameraController
    {
        public CameraControllerBase()
        {
            cameraPosition = Transform3D.Identity;
        }

        public CameraControllerBase(Transform3D initialCameraPosition)
        {
            cameraPosition = initialCameraPosition;
        }

        /// <summary>
        /// Gets the controlled position in world coordinates of the camera.
        /// </summary>
        public Transform3D CameraPosition
        {
            get => cameraPosition;
            protected set
            {
                cameraPosition = value;
                NewPositionCalculated?.Invoke(this, new NewPositionCalculatedEventArgs(cameraPosition));
            }
        }

        /// <summary>
        /// Flag to designate if the controller is activated or not.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Event fired when a new camera position has been calculated.
        /// </summary>
        public event NewPositionCalculatedEventHandler? NewPositionCalculated;

        private Transform3D cameraPosition;
    }
}
