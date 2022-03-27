using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a fixed XYZ rotation.
    /// </summary>
    public class FixedRotation3D : Rotation3D
    {
        public FixedRotation3D()
        {
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
            BuildFromValues();
        }

        public FixedRotation3D(Matrix<double> rotationMatrix)
        {
            matrix = rotationMatrix.Clone();
            ExtractValuesFromMatrix();
        }

        public FixedRotation3D(Rotation3D rotation)
        {
            matrix = rotation.Matrix.Clone();
            ExtractValuesFromMatrix();
        }

        public FixedRotation3D(
            double rx,
            double ry,
            double rz)
        {
            this.rx = rx;
            this.ry = ry;
            this.rz = rz;
            BuildFromValues();
        }

        public double Rx
        {
            get => rx;
            set
            {
                rx = value;
                BuildFromValues();
            }
        }

        public double Ry
        {
            get => ry;
            set
            {
                ry = value;
                BuildFromValues();
            }
        }

        public double Rz
        {
            get => rz;
            set
            {
                rz = value;
                BuildFromValues();
            }
        }

        public override string ToString()
        {
            return $"Rx {Rx:F3}, Ry {Ry:F3}, Rz {Rz:F3}";
        }

        private void BuildFromValues()
        {
            matrix = Matrix<double>.Build.DenseIdentity(3);

            double sx = Math.Sin(rx.ToRad());
            double sy = Math.Sin(ry.ToRad());
            double sz = Math.Sin(rz.ToRad());
            double cx = Math.Cos(rx.ToRad());
            double cy = Math.Cos(ry.ToRad());
            double cz = Math.Cos(rz.ToRad());

            matrix[0, 0] = cy * cz;
            matrix[0, 1] = cz * sx * sy - cx * sz;
            matrix[0, 2] = cx * cz * sy + sx * sz;
            matrix[1, 0] = cy * sz;
            matrix[1, 1] = cx * cz + sx * sy * sz;
            matrix[1, 2] = -cz * sx + cx * sy * sz;
            matrix[2, 0] = -sy;
            matrix[2, 1] = cy * sx;
            matrix[2, 2] = cx * cy;
        }

        private void ExtractValuesFromMatrix()
        {
            if (matrix[2, 0] < 1)
            {
                if (matrix[2, 0] > -1)
                {
                    ry = Math.Asin(-matrix[2, 0]).ToDeg();
                    rz = Math.Atan2(matrix[1, 0] / Math.Cos(ry), matrix[0, 0] / Math.Cos(ry)).ToDeg();
                    rx = Math.Atan2(matrix[2, 1] / Math.Cos(ry), x: matrix[2, 2] / Math.Cos(ry)).ToDeg();
                }
                else
                {
                    ry = (Math.PI / 2).ToDeg();
                    rz = 0.0;
                    rx = Math.Atan2(matrix[0, 1], matrix[0, 2]).ToDeg();
                }
            }
            else
            {
                ry = (-Math.PI / 2).ToDeg();
                rz = 0.0;
                rx = Math.Atan2(-matrix[0, 1], -matrix[0, 2]).ToDeg();
            }
        }

        private double rx;
        private double ry;
        private double rz;
    }
}
