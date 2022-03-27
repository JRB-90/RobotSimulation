using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents an axis angle rotation.
    /// </summary>
    public class AxisAngleRotation3D : Rotation3D
    {
        public AxisAngleRotation3D()
        {
            axis = new Vector3D(1.0, 0.0, 0.0);
            angle = 0.0;
            BuildFromValues();
        }

        public AxisAngleRotation3D(
            Vector3D axis, 
            double angle)
        {
            this.axis = axis;
            this.angle = angle;
            BuildFromValues();
        }

        public AxisAngleRotation3D(Matrix<double> rotationMatrix)
        {
            matrix = rotationMatrix.Clone();
            axis = Vector3D.Origin;
            ExtractValuesFromMatrix();
        }

        public AxisAngleRotation3D(Rotation3D rotation)
        {
            matrix = rotation.Matrix.Clone();
            axis = Vector3D.Origin;
            ExtractValuesFromMatrix();
        }

        public Vector3D Axis
        {
            get => axis;
            set
            {
                axis = value;
                BuildFromValues();
            }
        }

        public double Angle
        {
            get => angle;
            set
            {
                angle = value;
                BuildFromValues();
            }
        }

        private void BuildFromValues()
        {
            matrix = Matrix<double>.Build.DenseIdentity(3);

            double c = Math.Cos(angle.ToRad());
            double s = Math.Sin(angle.ToRad());

            double x2 = axis.X * axis.X;
            double y2 = axis.Y * axis.Y;
            double z2 = axis.Z * axis.Z;
            double xym = axis.X * axis.Y * (1 - c);
            double xzm = axis.X * axis.Z * (1 - c);
            double yzm = axis.Y * axis.Z * (1 - c);
            double xSin = axis.X * s;
            double ySin = axis.Y * s;
            double zSin = axis.Z * s;

            matrix[0, 0] = x2 * (1 - c) + c;
            matrix[0, 1] = xym - zSin;
            matrix[0, 2] = xzm + ySin;
            matrix[1, 0] = xym + zSin;
            matrix[1, 1] = y2 * (1 - c) + c;
            matrix[1, 2] = yzm - xSin;
            matrix[2, 0] = xzm - ySin;
            matrix[2, 1] = yzm + xSin;
            matrix[2, 2] = z2 * (1 - c) + c;
        }

        private void ExtractValuesFromMatrix()
        {
            double trace = matrix.Trace();
            if (trace > 3 && Math.Abs(trace - 3) < 1e-10)
            {
                trace = 3;
            }
            else if (trace < -1 && Math.Abs(trace + 1) < 1e-10)
            {
                trace = -1;
            }

            double t = (trace - 1) / 2;
            double tmpAngle = Math.Acos(t);

            if (tmpAngle > 1e-6)
            {
                if (tmpAngle < Math.PI)
                {
                    axis =
                        new Vector3D(
                            matrix[2, 1] - matrix[1, 2],
                            matrix[0, 2] - matrix[2, 0],
                            matrix[1, 0] - matrix[0, 1]
                        ).Normalised;
                }
                else
                {
                    double halfInverse;
                    if (matrix[0, 0] >= matrix[1, 1])
                    {
                        if (matrix[0, 0] >= matrix[2, 2])
                        {
                            var axisX = 0.5 * Math.Sqrt(1 + matrix[0, 0] - matrix[1, 1] - matrix[2, 2]);
                            halfInverse = axisX / 2;
                            var axisY = halfInverse * matrix[0, 1];
                            var axisZ = halfInverse * matrix[0, 2];
                            axis = new Vector3D(axisX, axisY, axisZ).Normalised;
                        }
                        else
                        {
                            var axisZ = 0.5 * Math.Sqrt(1 + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
                            halfInverse = axisZ / 2;
                            var axisX = halfInverse * matrix[0, 2];
                            var axisY = halfInverse * matrix[1, 2];
                            axis = new Vector3D(axisX, axisY, axisZ).Normalised;
                        }
                    }
                    else
                    {
                        if (matrix[1, 1] >= matrix[2, 2])
                        {
                            var axisY = 0.5 * Math.Sqrt(1 + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
                            halfInverse = axisY / 2;
                            var axisX = halfInverse * matrix[0, 1];
                            var axisZ = halfInverse * matrix[1, 2];
                            axis = new Vector3D(axisX, axisY, axisZ).Normalised;
                        }
                        else
                        {
                            var axisZ = 0.5 * Math.Sqrt(1 + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
                            halfInverse = axisZ / 2;
                            var axisX = halfInverse * matrix[0, 2];
                            var axisY = halfInverse * matrix[1, 2];
                            axis = new Vector3D(axisX, axisY, axisZ).Normalised;
                        }
                    }
                }
            }
            else
            {
                tmpAngle = 0;
                axis = new Vector3D(1.0, 0.0, 0.0);
            }

            angle = tmpAngle.ToDeg();
        }

        private Vector3D axis;
        private double angle;
    }
}
