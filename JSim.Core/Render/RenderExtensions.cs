namespace JSim.Core.Render
{
    /// <summary>
    /// Utility class to hold extension methods used in rendering.
    /// </summary>
    internal static class RenderExtensions
    {
        /// <summary>
        /// Converts an ARGB color byte to a equivalent float.
        /// </summary>
        /// <param name="value">Byte value.</param>
        /// <returns>Equivalent float value.</returns>
        public static float ArgbByteToFloat(this byte value)
        {
            return ((float)value) / ((float)byte.MaxValue);
        }

        /// <summary>
        /// Converts an ARGB color int to a equivalent float.
        /// </summary>
        /// <param name="value">Int value.</param>
        /// <returns>Equivalent float value.</returns>
        public static float ArgbIntToFloat(this byte value)
        {
            return ((float)value) / (255.0f);
        }

        /// <summary>
        /// Converts an ARBG color float into the equivalent byte.
        /// </summary>
        /// <param name="value">Float value to convert.</param>
        /// <returns>Equivalent byte value.</returns>
        public static byte ArgbFloatToByte(this float value)
        {
            if (value > 1.0f)
            {
                return 255;
            }
            else if (value < 0.0f)
            {
                return 0;
            }
            else
            {
                return (byte)(value / (1.0f / 255));
            }
        }

        /// <summary>
        /// Multiplies a Color by a scalar value.
        /// </summary>
        /// <param name="color">Color to scale.</param>
        /// <param name="scalar">Sclaar to apply.</param>
        /// <returns>Scaled color.</returns>
        public static Color MultiplyByScalar(
            this Color color, 
            float scalar)
        {
            float a = color.A * scalar;
            float r = color.R * scalar;
            float g = color.G * scalar;
            float b = color.B * scalar;

            return 
                new Color(
                    ConvertColorFloatToInt(a),
                    ConvertColorFloatToInt(r),
                    ConvertColorFloatToInt(g),
                    ConvertColorFloatToInt(b)
                );
        }

        /// <summary>
        /// Converts a float to an ARGB int.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Converted int.</returns>
        public static int ConvertColorFloatToInt(this float value)
        {
            if (value >= 255.0f)
            {
                return 255;
            }
            else if (value <= 0.0f)
            {
                return 0;
            }
            else
            {
                return (int)value;
            }
        }
    }
}
