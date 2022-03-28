using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a homogenous transformation that transforms the position
    /// and orientation of one 6DoF object to another.
    /// </summary>
    public class Transform3D
    {
        readonly Matrix<double> matrix;

        public Transform3D()
        {
            matrix = Matrix<double>.Build.DenseIdentity(4);
        }

        public Transform3D(Transform3D transform)
        {
            matrix = transform.Matrix.Clone();
        }

        public Transform3D(Matrix<double> transformationMatrix)
        {
            if (transformationMatrix.RowCount != 4 ||
                transformationMatrix.ColumnCount != 4)
            {
                throw new ArgumentException("Tranformation matrix must have size 4x4");
            }

            matrix = transformationMatrix.Clone();
        }

        public Transform3D(Vector3D translation)
        {
            matrix = Matrix<double>.Build.DenseIdentity(4);

            matrix[0, 3] = translation.X;
            matrix[1, 3] = translation.Y;
            matrix[2, 3] = translation.Z;
        }

        public Transform3D(Rotation3D rotation)
        {
            matrix = Matrix<double>.Build.DenseIdentity(4);

            matrix.SetSubMatrix(0, 0, rotation.Matrix);
        }

        public Transform3D(
            Vector3D translation,
            Rotation3D rotation)
        {
            matrix = Matrix<double>.Build.DenseIdentity(4);

            matrix[0, 3] = translation.X;
            matrix[1, 3] = translation.Y;
            matrix[2, 3] = translation.Z;

            matrix.SetSubMatrix(0, 0, rotation.Matrix);
        }

        public Vector3D Translation
        {
            get
            {
                return 
                    new Vector3D(
                        matrix[0, 3],
                        matrix[1, 3],
                        matrix[2, 3]
                    );
            }
            set
            {
                matrix[0, 3] = value.X;
                matrix[1, 3] = value.Y;
                matrix[2, 3] = value.Z;
            }
        }

        public Rotation3D Rotation
        {
            get
            {
                return
                    new Rotation3D(
                        matrix.SubMatrix(
                            0, 3,
                            0, 3
                        )
                    );
            }
            set
            {
                matrix.SetSubMatrix(
                    0, 
                    0, 
                    value.Matrix
                );
            }
        }

        public Matrix<double> Matrix =>
            matrix;

        public Transform3D Inverse =>
            new Transform3D(matrix.Inverse());

        public static Transform3D Identity =>
            new Transform3D();

        public static Transform3D RelativeTransform(
            Transform3D from,
            Transform3D to)
        {
            return from.Inverse * to;
        }

        public static Transform3D operator *(
            Transform3D left,
            Vector3D right)
        {
            var transMatrix = Matrix<double>.Build.DenseIdentity(4);
            transMatrix.SetColumn(3, 0, 3, right.Vector);

            return new Transform3D(left.Matrix * transMatrix);
        }

        public static Transform3D operator *(
            Transform3D left,
            Rotation3D right)
        {
            var rotMatrix = Matrix<double>.Build.DenseIdentity(4);
            rotMatrix.SetSubMatrix(0, 0, right.Matrix);

            return new Transform3D(left.Matrix * rotMatrix);
        }

        public static Transform3D operator *(
            Transform3D left, 
            Transform3D right)
        {
            return new Transform3D(left.Matrix * right.Matrix);
        }

        public override string ToString()
        {
            return $"{Translation}, {new FixedRotation3D(Rotation)}";
        }
    }
}
