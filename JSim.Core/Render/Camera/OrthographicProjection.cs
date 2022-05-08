using JSim.Core.Maths;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Class representing an orthographic projection.
    /// </summary>
    public class OrthographicProjection : CameraProjectionBase
    {
        public OrthographicProjection(
            int width,
            int height,
            double fovX,
            double fovY,
            double nearClip,
            double farClip)
          : 
            base(
                width, 
                height)
        {
            this.width = width;
            this.height = height;
            this.fovX = fovX.ToRad();
            this.fovY = fovY.ToRad();
            this.nearClip = nearClip;
            this.farClip = farClip;
        }

        /// <summary>
        /// Camera horixontal field of view.
        /// </summary>
        public double FovX
        {
            get => fovX;
            set
            {
                fovX = value;
                FireProjectionChangedEvent();
            }
        }

        /// <summary>
        /// Camera vertical field of view.
        /// </summary>
        public double FovY
        {
            get => fovY;
            set
            {
                fovY = value;
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

        public override Matrix<double> GetProjectionMatrix(ICamera camera)
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            var camToOrigin = Vector3D.Origin - camera.PositionInWorld.Translation;
            var angleA = camera.PositionInWorld.K.AngleWith(camToOrigin);
            var angleB = 90.0 - angleA;
            var angleC = 180.0 - angleA - angleB;
            var scalingFactor =
                (camToOrigin.Length * Math.Sin(angleB.ToRad())) /
                Math.Sin(angleC.ToRad());
            scalingFactor = Math.Abs(scalingFactor);

            mat[0, 0] = (1.0 / scalingFactor) / Math.Tan(fovX / 2.0) / AspectRatio;
            mat[1, 1] = (1.0 / scalingFactor) / Math.Tan(fovY / 2.0);
            mat[2, 2] = -2.0 / (farClip - nearClip);
            mat[2, 3] = -(farClip + nearClip) / (farClip - nearClip);

            return mat;
        }

        private double fovX;
        private double fovY;
        private double nearClip;
        private double farClip;
    }
}
