using JSim.Core.Maths;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Class representing a perspective projection.
    /// </summary>
    public class PerspectiveProjection : CameraProjectionBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of the renderable area in pixels.</param>
        /// <param name="height">Height of the renderable area in pixels.</param>
        /// <param name="fov">Camera field of view.</param>
        /// <param name="nearClip">Near cliping plane distance.</param>
        /// <param name="farClip">Far clipping plane distance.</param>
        public PerspectiveProjection(
            int width, 
            int height, 
            double fov, 
            double nearClip, 
            double farClip)
          : 
            base(
                width, 
                height)
        {
            this.width = width;
            this.height = height;
            this.fov = fov;
            this.nearClip = nearClip;
            this.farClip = farClip;
        }

        /// <summary>
        /// Camera field of view.
        /// </summary>
        public double Fov
        {
            get => fov;
            set
            {
                fov = value;
                FireProjectionChangedEvent();
            }
        }

        /// <summary>
        /// Near clipping plane distance.
        /// </summary>
        public double NearClip
        {
            get => nearClip;
            set
            {
                nearClip = value;
                FireProjectionChangedEvent();
            }
        }

        /// <summary>
        /// Far clipping plane distance.
        /// </summary>
        public double FarClip
        {
            get => farClip;
            set
            {
                farClip = value;
                FireProjectionChangedEvent();
            }
        }

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        /// <returns>Projection matrix.</returns>
        public override Matrix<double> GetProjectionMatrix(ICamera camera)
        {
            Matrix<double> result = Matrix<double>.Build.DenseDiagonal(4, 0.0);
            
            double ar = (double)width / (double)height;
            double t = 1.0 / Math.Tan(fov.ToRad() / 2.0);

            result[0, 0] = t / ar;
            result[1, 1] = t;
            result[2, 2] = -(farClip + nearClip) / (farClip - nearClip);
            result[2, 3] = -2.0 * (nearClip * farClip) / (farClip - nearClip);
            result[3, 2] = -1.0;

            return result;
        }

        private double fov;
        private double nearClip;
        private double farClip;
    }
}
