using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a cartesian vector.
    /// </summary>
    public class Vector3D
    {
        readonly Vector<double> vector;

        public Vector3D()
        {
            vector = Vector<double>.Build.Dense(3, 0.0);
        }

        public Vector3D(Vector<double> vector)
        {
            if (vector.Count != 3)
            {
                throw new ArgumentException("Vector must be of size 3");
            }

            this.vector = vector;
        }

        public Vector3D(Vector3D vector)
        {
            this.vector =
                Vector<double>.Build.Dense(
                    new double[]
                    {
                        vector.X,
                        vector.Y,
                        vector.Z
                    }
                );
        }

        public Vector3D(
            double x,
            double y,
            double z)
        {
            vector =
                Vector<double>.Build.Dense(
                    new double[]
                    {
                        x,
                        y,
                        z
                    }
                );
        }

        public double X
        {
            get => vector[0];
            set => vector[0] = value;
        }

        public double Y
        {
            get => vector[1];
            set => vector[1] = value;
        }

        public double Z
        {
            get => vector[2];
            set => vector[2] = value;
        }

        public double Length =>
            vector.L2Norm();

        public double Norm =>
            vector.Norm(Length);

        public Vector3D Normalised =>
            new Vector3D(vector.Normalize(Length));

        public Vector<double> Vector =>
            vector;

        public static Vector3D Origin =>
            new Vector3D();

        public Vector3D Cross(Vector3D right)
        {
            Vector<double> result = Vector<double>.Build.Dense(3);
            result[0] = this.vector[1] * right.Vector[2] - this.vector[2] * right.Vector[1];
            result[1] = -this.vector[0] * right.Vector[2] + this.vector[2] * right.Vector[0];
            result[2] = this.vector[0] * right.Vector[1] - this.vector[1] * right.Vector[0];

            return new Vector3D(result);
        }

        public double Dot(Vector3D right)
        {
            return vector.DotProduct(right.Vector);
        }

        public static Vector3D operator+(
            Vector3D left,
            Vector3D right)
        {
            return new Vector3D(left.vector + right.vector);
        }

        public static Vector3D operator -(
            Vector3D left,
            Vector3D right)
        {
            return new Vector3D(left.vector - right.vector);
        }

        public static Vector3D operator *(
            Vector3D left,
            Transform3D right)
        {
            Vector<double> v =
                Vector<double>.Build.DenseOfArray(
                    new double[]
                    {
                        left.X,
                        left.Y,
                        left.Z,
                        1.0
                    }
                );

            var result = v * right.Matrix;

            return new Vector3D(result.SubVector(0, 3));
        }

        public override string ToString()
        {
            return $"X: {X:F3}, Y: {Y:F3}, Z: {Z:F3}";
        }
    }
}
