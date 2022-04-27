using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a rotation in 3D space.
    /// </summary>
    public class Rotation3D
    {
        public Rotation3D()
        {
            matrix = Matrix<double>.Build.DenseIdentity(3);
        }

        public Rotation3D(Rotation3D rotation)
        {
            matrix = rotation.matrix.Clone();
        }

        public Rotation3D(Matrix<double> rotationMatrix)
        {
            if (rotationMatrix.RowCount != 3 ||
                rotationMatrix.ColumnCount != 3)
            {
                throw new ArgumentException("Rotation matrix must have size 3x3");
            }

            matrix = rotationMatrix.Clone();
        }

        public Rotation3D(
            Vector3D unitX, 
            Vector3D unitY, 
            Vector3D unitZ)
        {
            matrix = Matrix<double>.Build.DenseIdentity(3);

            matrix.SetColumn(
                0,
                new double[]
                {
                    unitX.X,
                    unitX.Y,
                    unitX.Z
                }
            );

            matrix.SetColumn(
                1,
                new double[]
                {
                    unitY.X,
                    unitY.Y,
                    unitY.Z
                }
            );

            matrix.SetColumn(
                2,
                new double[]
                {
                    unitZ.X,
                    unitZ.Y,
                    unitZ.Z
                }
            );
        }

        public Matrix<double> Matrix => matrix;

        public static Rotation3D Identity => new Rotation3D();

        public Rotation3D Inverse =>
           new Rotation3D(matrix.Inverse());

        public static Rotation3D operator *(
            Rotation3D left,
            Rotation3D right)
        {
            return new Rotation3D(left.Matrix * right.Matrix);
        }

        protected Matrix<double> matrix;
    }
}
