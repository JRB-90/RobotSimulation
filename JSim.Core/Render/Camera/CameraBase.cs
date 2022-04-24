using JSim.Core.Display;
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
            return projectionMatrix;
        }

        /// <summary>
        /// Gets the camera view amtrix.
        /// </summary>
        /// <returns>Matrix object representing the view matrix.</returns>
        public Matrix<double> GetViewMatrix()
        {
            return viewMatrix;
        }

        /// <summary>
        /// Updates the camera with information about the rendering surface.
        /// </summary>
        /// <param name="surface">Rendering surface to be used to update the camera.</param>
        public void Update(IRenderingSurface surface)
        {
            cameraProjection.Height = surface.SurfaceHeight;
            cameraProjection.Width = surface.SurfaceWidth;
            projectionMatrix = cameraProjection.GetProjectionMatrix();
            viewMatrix = cameraProjection.GetViewMatrix(this);
            FireCameraModifiedEvent();
        }

        protected void FireCameraModifiedEvent()
        {
            CameraModified?.Invoke(this, new CameraModifiedEventArgs());
        }

        private void OnProjectionModified(object sender, ProjectionModifiedEventArgs e)
        {
            FireCameraModifiedEvent();
        }

        private Transform3D positionInWorld;
        private ICameraProjection cameraProjection;
        private Matrix<double> projectionMatrix;
        private Matrix<double> viewMatrix;
    }
}
