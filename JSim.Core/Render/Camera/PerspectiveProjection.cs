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
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of the renderable area in pixels.</param>
        /// <param name="height">Height of the renderable area in pixels.</param>
        /// <param name="fov">Camera field of view.</param>
        /// <param name="nearClip">Near cliping plane distance.</param>
        /// <param name="farClip">Far clipping plane distance.</param>
        public PerspectiveProjection(int width, int height, double fov, double nearClip, double farClip)
            : base(width, height)
        {
            this.width = width;
            this.height = height;
            this.fov = fov.ToRad();
            this.nearClip = nearClip;
            this.farClip = farClip;
        }

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        /// <returns>Projection matrix.</returns>
        public override Matrix<double> GetProjectionMatrix()
        {
            Matrix<double> result = Matrix<double>.Build.DenseDiagonal(4, 0.0);
            result[0, 0] = (1.0 / Math.Tan(fov / 2.0)) / AspectRatio;
            result[1, 1] = (1.0 / Math.Tan(fov / 2.0));
            result[2, 2] = (nearClip + farClip) / (nearClip - farClip);
            result[2, 3] = (2.0 * nearClip * farClip) / (nearClip - farClip);
            result[3, 2] = -1.0;

            return result;
        }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <param name="camera">Camera calculate the view matrix from.</param>
        /// <returns>View matrix.</returns>
        public override Matrix<double> GetViewMatrix(ICamera camera)
        {
            Matrix<double> result = Matrix<double>.Build.DenseIdentity(4);
            Matrix<double> translation = -camera.PositionInWorld.Translation.ToHomogeneousMat();
            Matrix<double> rotation = Matrix<double>.Build.DenseIdentity(4);

            rotation[0, 0] = camera.PositionInWorld.I.X;
            rotation[0, 1] = camera.PositionInWorld.I.Y; 
            rotation[0, 2] = camera.PositionInWorld.I.Z;

            rotation[1, 0] = camera.PositionInWorld.J.X; 
            rotation[1, 1] = camera.PositionInWorld.J.Y; 
            rotation[1, 2] = camera.PositionInWorld.J.Z;

            rotation[2, 0] = camera.PositionInWorld.K.X; 
            rotation[2, 1] = camera.PositionInWorld.K.Y; 
            rotation[2, 2] = camera.PositionInWorld.K.Z;

            return rotation * translation;
        }

        private double fov;
        private double nearClip;
        private double farClip;
    }
}
