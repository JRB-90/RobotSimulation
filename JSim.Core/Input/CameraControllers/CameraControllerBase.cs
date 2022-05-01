using JSim.Core.Render;

namespace JSim.Core.Input
{
    /// <summary>
    /// Base class for sharing functionality between camera controller implementations.
    /// </summary>
    public abstract class CameraControllerBase : ICameraController
    {
        public CameraControllerBase()
        {
        }

        public CameraControllerBase(ICamera? camera)
        {
            this.camera = camera;
        }

        /// <summary>
        /// The camera this controller is manipulating.
        /// </summary>
        public ICamera? AttachedCamera
        {
            get => camera;
            set
            {
                camera = value;
                OnCameraChanged(camera);
            }
        }

        /// <summary>
        /// Called when the camera object has changed.
        /// </summary>
        /// <param name="camera">New camera object.</param>
        protected abstract void OnCameraChanged(ICamera? camera);

        private ICamera? camera;
    }
}
