using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    public static class MathExtensions
    {
        public static double ToRad(this double degrees)
        {
            return (degrees * Math.PI) / 180.0;
        }

        public static double ToDeg(this double radians)
        {
            return (radians * 180.0) / Math.PI;
        }

        public static Rotation3D AsRotation3D(this Rotation3D rotation)
        {
            return new Rotation3D(rotation);
        }

        public static FixedRotation3D AsFixed(this Rotation3D rotation)
        {
            return new FixedRotation3D(rotation);
        }

        public static AxisAngleRotation3D AsAxisAngle(this Rotation3D rotation)
        {
            return new AxisAngleRotation3D(rotation);
        }

        public static Matrix<double> ToHomogeneousMat(this Vector3D translation)
        {
            return new Transform3D(translation).Matrix;
        }

        public static Transform3D ToHomogeneousTransform(this Vector3D translation)
        {
            return new Transform3D(translation);
        }
    }
}
