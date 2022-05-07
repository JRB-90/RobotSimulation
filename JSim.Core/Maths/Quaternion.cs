using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a rotation as a quaternion.
    /// </summary>
    public class Quaternion
    {
        public Quaternion()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            W = 1.0;
        }

        public Quaternion(Quaternion q)
        {
            X = q.X;
            Y = q.Y;
            Z = q.Z;
            W = q.W;
        }

        public Quaternion(
            double x,
            double y,
            double z,
            double w)
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            W = 1.0;
        }

        public Quaternion(Rotation3D rotation)
          : 
            this(rotation.Matrix)
        {
        }

        public Quaternion(Matrix<double> rotationMatrix)
        {
            if (rotationMatrix.ColumnCount != 3 ||
                rotationMatrix.RowCount != 3)
            {
                throw new ArgumentException("Rotation matrix must be 3x3");
            }

            double t = 
                rotationMatrix[0, 0] + 
                rotationMatrix[1, 1] + 
                rotationMatrix[2, 2] + 
                1;

            if (Math.Abs(t) < 1e-10)
            {
                if (rotationMatrix[0, 0] > rotationMatrix[1, 1] && 
                    rotationMatrix[0, 0] > rotationMatrix[2, 2])
                {
                    double s = 
                        Math.Sqrt(
                            1 + rotationMatrix[0, 0] - rotationMatrix[1, 1] - rotationMatrix[2, 2]
                        ) * 2;

                    X = 0.25 * s;
                    Y = (rotationMatrix[1, 0] + rotationMatrix[0, 1]) / s;
                    Z = (rotationMatrix[0, 2] + rotationMatrix[2, 0]) / s;
                    W = (rotationMatrix[2, 1] - rotationMatrix[1, 2]) / s;
                }
                else if (rotationMatrix[1, 1] > rotationMatrix[0, 0] &&
                         rotationMatrix[1, 1] > rotationMatrix[2, 2])
                {
                    double s = 
                        Math.Sqrt(
                            1 + rotationMatrix[1, 1] - rotationMatrix[0, 0] - rotationMatrix[2, 2]
                        ) * 2;

                    X = (rotationMatrix[1, 0] + rotationMatrix[0, 1]) / s;
                    Y = 0.25 * s;
                    Z = (rotationMatrix[2, 1] + rotationMatrix[1, 2]) / s;
                    W = (rotationMatrix[0, 2] - rotationMatrix[2, 0]) / s;
                }
                else
                {
                    double s = 
                        Math.Sqrt(
                            1 + rotationMatrix[2, 2] - rotationMatrix[0, 0] - rotationMatrix[1, 1]
                        ) * 2;

                    X = (rotationMatrix[0, 2] + rotationMatrix[2, 0]) / s;
                    Y = (rotationMatrix[2, 1] + rotationMatrix[1, 2]) / s;
                    Z = 0.25 * s;
                    W = (rotationMatrix[1, 0] - rotationMatrix[0, 1]) / s;
                }
            }
            else
            {
                double s = Math.Sqrt(t) * 2;

                X = (rotationMatrix[2, 1] - rotationMatrix[1, 2]) / s;
                Y = (rotationMatrix[0, 2] - rotationMatrix[2, 0]) / s;
                Z = (rotationMatrix[1, 0] - rotationMatrix[0, 1]) / s;
                W = 0.25 * s;
            }
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double W { get; set; }

        public double Length =>
            Math.Sqrt(SquaredLength);

        public double SquaredLength =>
            X * X + Y * Y + Z * Z + W * W;

        public Quaternion Conjugate =>
            new Quaternion(-X, -Y, -Z, W);

        public Quaternion Normalised
        {
            get
            {
                var q = new Quaternion(this);
                q.Normalise();

                return q;
            }
        }

        public Quaternion Inverse
        {
            get
            {
                if (SquaredLength < 1e-10)
                {
                    return new Quaternion(double.NaN, double.NaN, double.NaN, double.NaN);
                }
                else
                {
                    return Conjugate / SquaredLength;
                }
            }
        }

        public void Normalise()
        {
            if (Length < 1e-10)
            {
                X = 0;
                Y = 0;
                Z = 0;
                W = 0;
            }
            else
            {
                X = X / Length;
                Y = Y / Length;
                Z = Z / Length;
                W = W / Length;
            }
        }

        public Rotation3D ToRotation3D()
        {
            return new Rotation3D(ToMatrix());
        }

        public Matrix<double> ToMatrix()
        {
            var q = this.Normalised;

            double xx = q.X * q.X;
            double xy = q.X * q.Y;
            double xz = q.X * q.Z;
            double xw = q.X * q.W;
            double yy = q.Y * q.Y;
            double yz = q.Y * q.Z;
            double yw = q.Y * q.W;
            double zz = q.Z * q.Z;
            double zw = q.Z * q.W;

            var matrix = Matrix<double>.Build.DenseIdentity(3);
            matrix[0, 0] = 1 - 2 * (yy + zz);
            matrix[0, 1] = 2 * (xy - zw);
            matrix[0, 2] = 2 * (xz + yw);
            matrix[1, 0] = 2 * (xy + zw);
            matrix[1, 1] = 1 - 2 * (xx + zz);
            matrix[1, 2] = 2 * (yz - xw);
            matrix[2, 0] = 2 * (xz - yw);
            matrix[2, 1] = 2 * (yz + xw);
            matrix[2, 2] = 1 - 2 * (xx + yy);

            return matrix;
        }

        public static Quaternion RotationAboutAxis(Vector3D axis, double angle)
        {
            Vector3D normalisedAxis = new Vector3D(axis).Normalised;
            double halfAngle = 0.5 * angle.ToRad();
            double sinHalfAngle = Math.Sin(halfAngle);

            return
                new Quaternion(
                    sinHalfAngle * normalisedAxis.X,
                    sinHalfAngle * normalisedAxis.Y,
                    sinHalfAngle * normalisedAxis.Z,
                    Math.Cos(halfAngle)
                );
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return 
                new Quaternion(
                    lhs.W * rhs.X + lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                    lhs.W * rhs.Y + lhs.Y * rhs.W + lhs.Z * rhs.X - lhs.X * rhs.Z,
                    lhs.W * rhs.Z + lhs.Z * rhs.W + lhs.X * rhs.Y - lhs.Z * rhs.Y,
                    lhs.W * rhs.W - lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z
                );
        }

        public static Quaternion operator *(Quaternion q, double scalar)
        {
            return
                new Quaternion(
                    q.X * scalar,
                    q.Y * scalar,
                    q.Z * scalar,
                    q.W * scalar
                );
        }

        public static Quaternion operator *(double scalar, Quaternion q)
        {
            return q * scalar;
        }

        public static Quaternion operator /(Quaternion q, double scalar)
        {
            return
                new Quaternion(
                    q.X / scalar,
                    q.Y / scalar,
                    q.Z / scalar,
                    q.W / scalar
                );
        }

        public static Quaternion operator /(double scalar, Quaternion q)
        {
            return q / scalar;
        }
    }
}
