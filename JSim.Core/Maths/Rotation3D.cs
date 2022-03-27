﻿using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a rotation in 3D space.
    /// </summary>
    public class Rotation3D
    {
        readonly Matrix<double> matrix;

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

        public Matrix<double> Matrix => matrix;

        public static Rotation3D Identity => new Rotation3D();
    }
}
