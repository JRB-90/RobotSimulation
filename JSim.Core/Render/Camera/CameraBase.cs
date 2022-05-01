using JSim.Core.Input;
using JSim.Core.Maths;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Abstract base class for cameras.
    /// </summary>
    public abstract class CameraBase : ICamera
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cameraProjection">Projection to use for the camera.</param>
        public CameraBase(ICameraProjection cameraProjection)
        {
            this.cameraProjection = cameraProjection;
            cameraProjection.ProjectionModified += OnProjectionModified;
            positionInWorld = Transform3D.Identity;
            projectionMatrix = Matrix<double>.Build.DenseIdentity(4);
            viewMatrix = Matrix<double>.Build.DenseIdentity(4);
        }

        /// <summary>
        /// The controller used for manipulating the camera.
        /// </summary>
        public ICameraController? CameraController
        {
            get => cameraController;
            set
            {
                cameraController = value;
                if (cameraController != null)
                {
                    cameraController.NewPositionCalculated += OnNewPositionCalculated;
                }
                FireCameraModifiedEvent();
            }
        }

        /// <summary>
        /// Gets and sets the position of the camera in the world.
        /// </summary>
        public Transform3D PositionInWorld
        {
            get => positionInWorld;
            set
            {
                positionInWorld = value;
                FireCameraModifiedEvent();
            }
        }

        /// <summary>
        /// Cameras projection.
        /// </summary>
        public ICameraProjection CameraProjection
        {
            get => cameraProjection;
            set
            {
                cameraProjection = value;
                cameraProjection.ProjectionModified += OnProjectionModified;
                FireCameraModifiedEvent();
            }
        }

        /// <summary>
        /// Event fired when the camera parameters have changed.
        /// </summary>
        public event CameraModifiedEventHandler? CameraModified;

        /// <summary>
        /// Gets the camera projection matrix.
        /// </summary>
        /// <returns>MAtrix object representing the projection matrix.</returns>
        public Matrix<double> GetProjectionMatrix()
        {
            return cameraProjection.GetProjectionMatrix();
        }

        /// <summary>
        /// Gets the camera view amtrix.
        /// </summary>
        /// <returns>Matrix object representing the view matrix.</returns>
        public Matrix<double> GetViewMatrix()
        {
            return PositionInWorld.Inverse.Matrix;
        }

        /// <summary>
        /// Updates the camera with information about the rendering surface.
        /// </summary>
        /// <param name="surface">Rendering surface to be used to update the camera.</param>
        public void Update(IRenderingSurface surface)
        {
            if (cameraProjection.Height != surface.SurfaceHeight ||
                cameraProjection.Width != surface.SurfaceWidth)
            {
                cameraProjection.Height = surface.SurfaceHeight;
                cameraProjection.Width = surface.SurfaceWidth;
                FireCameraModifiedEvent();
            }
        }

        /// <summary>
        /// Rotates the camera to look at a given point.
        /// </summary>
        /// <param name="focusPoint">The point in space to focus the camera on.</param>
        /// <param name="up">The up vector of the world, in order to ensure the camera does not yaw.</param>
        public void LookAtPoint(Vector3D focusPoint, Vector3D up)
        {
            Vector3D zAxis = (PositionInWorld.Translation - focusPoint).Normalised;
            Vector3D xAxis = up.Cross(zAxis).Normalised;
            Vector3D yAxis = zAxis.Cross(xAxis).Normalised;

            PositionInWorld =
                new Transform3D(
                    PositionInWorld.Translation,
                    new Rotation3D(xAxis, yAxis, zAxis)
                );
        }

        private void OnNewPositionCalculated(object sender, NewPositionCalculatedEventArgs e)
        {
            PositionInWorld = e.NewPosition;
        }

        protected void FireCameraModifiedEvent()
        {
            CameraModified?.Invoke(this, new CameraModifiedEventArgs());
        }

        private void OnProjectionModified(object sender, ProjectionModifiedEventArgs e)
        {
            FireCameraModifiedEvent();
        }

        private ICameraController? cameraController;
        private Transform3D positionInWorld;
        private ICameraProjection cameraProjection;
        private Matrix<double> projectionMatrix;
        private Matrix<double> viewMatrix;
    }
}
