namespace JSim.Core.Maths
{
    internal static class MathExtensions
    {
        public static double ToRad(this double degrees)
        {
            return (degrees * Math.PI) / 180.0;
        }

        public static double ToDeg(this double radians)
        {
            return (radians * 180.0) / Math.PI;
        }
    }
}
