using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a 2 dimensional certesian vector.
    /// </summary>
    public class Vector2D
    {
        readonly Vector<double> vector;

        public Vector2D()
        {
            vector = Vector<double>.Build.Dense(2, 0.0);
        }

        public Vector2D(Vector<double> vector)
        {
            if (vector.Count != 2)
            {
                throw new ArgumentException("Vector must be of size 2");
            }

            this.vector = vector.Clone();
        }

        public Vector2D(Vector2D vector)
        {
            this.vector =
                Vector<double>.Build.Dense(
                    new double[]
                    {
                        vector.X,
                        vector.Y
                    }
                );
        }

        public Vector2D(
            double x,
            double y)
        {
            vector =
                Vector<double>.Build.Dense(
                    new double[]
                    {
                        x,
                        y
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

        public double Length =>
            vector.L2Norm();

        public double Norm =>
            vector.Norm(Length);

        public Vector2D Normalised =>
            new Vector2D(vector.Normalize(Length));

        public Vector<double> Vector =>
            vector;

        public static Vector2D Origin =>
            new Vector2D();

        public double Dot(Vector2D right)
        {
            return vector.DotProduct(right.Vector);
        }

        public static Vector2D operator +(
            Vector2D left,
            Vector2D right)
        {
            return new Vector2D(left.vector + right.vector);
        }

        public static Vector2D operator -(
            Vector2D left,
            Vector2D right)
        {
            return new Vector2D(left.vector - right.vector);
        }

        public override string ToString()
        {
            return $"X {X:F3}, Y {Y:F3}";
        }
    }
}
