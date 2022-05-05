using JSim.Core.Maths;
using System.Diagnostics;

namespace JSim.Core.Input
{
    /// <summary>
    /// Base class to share functionality for all implementations of an orbitting controller.
    /// </summary>
    public abstract class OrbitControllerBase : CameraControllerBase, IOrbitCameraController
    {
        const double DEFAULT_PAN_SPEED = 0.01;
        const double DEFAULT_ROT_SPEED = 0.1;
        const double DEFAULT_ZOOM_SPEED = 0.1;

        public OrbitControllerBase()
          :
            base()
        {
            focusPoint = Vector3D.Origin;
            upVector = Vector3D.UnitZ;
            panSpeed = DEFAULT_PAN_SPEED;
            rotationSpeed = DEFAULT_ROT_SPEED;
            zoomSpeed = DEFAULT_ZOOM_SPEED;
        }

        public OrbitControllerBase(Transform3D initialCameraPosition)
          :
            base(initialCameraPosition)
        {
            focusPoint = Vector3D.Origin;
            upVector = Vector3D.UnitZ;
            panSpeed = DEFAULT_PAN_SPEED;
            rotationSpeed = DEFAULT_ROT_SPEED;
            zoomSpeed = DEFAULT_ZOOM_SPEED;
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
       
        public double PanSpeed
        {
            get => panSpeed;
            set => panSpeed = value;
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

        protected void Pan(double horizontal, double vertical)
        {
            var newCamPos = 
                CameraPosition * 
                new Transform3D(
                    new Vector3D(-horizontal * panSpeed, vertical * panSpeed, 0.0),
                    Rotation3D.Identity
                );

            var shiftInWorld = newCamPos.Translation - CameraPosition.Translation;
            CameraPosition = newCamPos;
            FocusPoint += shiftInWorld;
            CameraPosition.Rotation = CalculateLookAtRotation(CameraPosition.Translation);
            FireNewPositionCalculatedEvent();
        }

        protected void Rotate(double horizontal, double vertical)
        {
            var spherical = new SphericalCoords(CameraPosition.Translation - FocusPoint);
            
            spherical.Azimuth -= (horizontal * rotationSpeed).ToRad();
            spherical.Elevation += (vertical * rotationSpeed).ToRad();
            spherical.Elevation = Math.Clamp(spherical.Elevation, (10.0).ToRad(), (170.0).ToRad());

            var newCameraPos = spherical.ToCartesian() + FocusPoint;
            var newCameraRot = CalculateLookAtRotation(newCameraPos);
            CameraPosition = new Transform3D(newCameraPos, newCameraRot);
            FireNewPositionCalculatedEvent();
        }

        /// <summary>
        /// Translates the camera position along its z azis towards the focuesd object.
        /// Movement is exponential and so slows down as you approach closer and speeds
        /// up as you get further away.
        /// </summary>
        /// <param name="zoomAmount">Direction and amount of zoom to complete.</param>
        protected void ZoomExponential(double zoomAmount)
        {
            var direction = (FocusPoint - CameraPosition.Translation);
            var shift = (direction * Math.Clamp(zoomAmount, -1.0, 1.0) * zoomSpeed);
            var newCameraPos = CameraPosition.Translation + shift;
            CameraPosition = new Transform3D(newCameraPos, CameraPosition.Rotation);
            FireNewPositionCalculatedEvent();
        }

        /// <summary>
        /// Rotates the camera to look at a given point.
        /// </summary>
        protected Rotation3D CalculateLookAtRotation(Vector3D cameraPos)
        {
            Vector3D zAxis = (cameraPos - FocusPoint).Normalised;
            Vector3D xAxis = UpVector.Cross(zAxis).Normalised;
            Vector3D yAxis = zAxis.Cross(xAxis).Normalised;

            return new Rotation3D(xAxis, yAxis, zAxis);
        }

        /// <summary>
        /// Called when internal parameters that have changed that would effect the camera position.
        /// Implementations should recalculate the camera position from the new parameters and update
        /// the camera position.
        /// </summary>
        protected abstract void OnParametersChanged();

        protected OrbitState orbitState;

        private Vector3D focusPoint;
        private Vector3D upVector;
        private double panSpeed;
        private double rotationSpeed;
        private double zoomSpeed;
    }
}
