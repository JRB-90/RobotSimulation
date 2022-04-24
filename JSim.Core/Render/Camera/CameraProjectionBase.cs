using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Abstract base class to provide common functionality for all camera projections.
    /// </summary>
    public abstract class CameraProjectionBase : ICameraProjection
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of the surface to project to in pixels.</param>
        /// <param name="height">Height of the surface to project to in pixels.</param>
        public CameraProjectionBase(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Gets and sets the width of the projection image.
        /// </summary>
        public int Width
        {
            get => width;
            set
            {
                width = value;
                FireProjectionChangedEvent();
            }
        }

        /// <summary>
        /// Gets and sets the height of the projection image.
        /// </summary>
        public int Height
        {
            get => height;
            set
            {
                height = value;
                FireProjectionChangedEvent();
            }
        }

        /// <summary>
        /// Image aspect ratio.
        /// </summary>
        public double AspectRatio =>
            (double)width / (double)height;

        /// <summary>
        /// Event fired when the projection parameters have been modified.
        /// </summary>
        public event ProjectionModifiedEventHandler? ProjectionModified;

        /// <summary>
        /// Gets the camera projection matrix.
        /// </summary>
        /// <returns>Projection matrix.</returns>
        public abstract Matrix<double> GetProjectionMatrix();

        /// <summary>
        /// Gets the camera view matrix.
        /// </summary>
        /// <param name="camera">Camera the view matrix is derived from.</param>
        /// <returns>View matrix.</returns>
        public abstract Matrix<double> GetViewMatrix(ICamera camera);

        protected void FireProjectionChangedEvent()
        {
            ProjectionModified?.Invoke(this, new ProjectionModifiedEventArgs());
        }

        protected int width;
        protected int height;
    }
}
