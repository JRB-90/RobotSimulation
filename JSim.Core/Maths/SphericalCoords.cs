namespace JSim.Core.Maths
{
    /// <summary>
    /// Represents a position in a spherical coordinate system.
    /// </summary>
    public class SphericalCoords
    {
        public SphericalCoords()
        {
            Azimuth = 0.0;
            Elevation = 0.0;
            Range = 0.0;
        }

        public SphericalCoords(
            double azimuth,
            double elevation,
            double range)
        {
            Azimuth = azimuth;
            Elevation = elevation;
            Range = range;
        }

        public SphericalCoords(Vector3D cartesian)
        {
            Azimuth = Math.Atan2(cartesian.Y, cartesian.X);

            Elevation =
                Math.Atan2(
                    Math.Sqrt(
                        (cartesian.X * cartesian.X) +
                        (cartesian.Y * cartesian.Y)
                    ),
                    cartesian.Z
                );

            Range = cartesian.Length;
        }

        public double Azimuth { get; set; }

        public double Elevation { get; set; }

        public double Range { get; set; }

        public Vector3D ToCartesian()
        {
            double x = Range * Math.Cos(Azimuth) * Math.Sin(Elevation);
            double y = Range * Math.Sin(Azimuth) * Math.Sin(Elevation);
            double z = Range * Math.Cos(Elevation);

            return new Vector3D(x, y, z);
        }

        public override string ToString()
        {
            return $"Az {Azimuth:F3}, El {Elevation:F3}, Ra {Range:F3}";
        }
    }
}
