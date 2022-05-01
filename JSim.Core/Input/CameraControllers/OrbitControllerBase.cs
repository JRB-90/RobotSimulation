using JSim.Core.Maths;

namespace JSim.Core.Input
{
    /// <summary>
    /// Base class to share functionality for all implementations of an orbitting controller.
    /// </summary>
    public abstract class OrbitControllerBase : CameraControllerBase, IOrbitCameraController
    {
        public OrbitControllerBase()
          :
            base()
        {
            focusPoint = Vector3D.Origin;
            upVector = Vector3D.UnitZ;
            rotationSpeed = 10.0;
            zoomSpeed = 1.0;
        }

        public OrbitControllerBase(Transform3D initialCameraPosition)
          :
            base(initialCameraPosition)
        {
            focusPoint = Vector3D.Origin;
            upVector = Vector3D.UnitZ;
            rotationSpeed = 10.0;
            zoomSpeed = 1.0;
        }

        public Vector3D FocusPoint
        {
            get => focusPoint;
            set
            {
                focusPoint = value;
                OnParametersChanged();
            }
        }
        
        public Vector3D UpVector
        {
            get => upVector;
            set
            {
                upVector = value.Normalised;
                OnParametersChanged();
            }
        }
       
        public double RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }
        
        public double ZoomSpeed
        {
            get => zoomSpeed;
            set => zoomSpeed = value;
        }

        /// <summary>
        /// Called when internal parameters that have changed that would effect the camera position.
        /// Implementations should recalculate the camera position from the new parameters and update
        /// the camera position.
        /// </summary>
        protected abstract void OnParametersChanged();

        private Vector3D focusPoint;
        private Vector3D upVector;
        private double rotationSpeed;
        private double zoomSpeed;
    }
}
