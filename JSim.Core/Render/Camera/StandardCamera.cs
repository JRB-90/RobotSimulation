namespace JSim.Core.Render
{
    /// <summary>
    /// Class to represent a standard viewport camera.
    /// </summary>
    public class StandardCamera : CameraBase
    {
        /// <summary>
        /// Constructor, creates a defualt perspective projection camera.
        /// </summary>
        public StandardCamera(int width, int height)
          : 
            base(
                new PerspectiveProjection(
                    width, 
                    height, 
                    90.0, 
                    0.001, 
                    10000.0
                )
            )
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cameraProjection">Projection to give the camera.</param>
        public StandardCamera(ICameraProjection cameraProjection)
          : 
            base(cameraProjection)
        {
        }
    }
}
