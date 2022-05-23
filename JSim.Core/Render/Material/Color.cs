namespace JSim.Core.Render
{
    /// <summary>
    /// Struct to hold color data.
    /// </summary>
    public struct Color
    {
        public Color(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="r">Red component of the color.</param>
        /// <param name="g">Green component of the color.</param>
        /// <param name="b">Blue component of the color.</param>
        public Color(
            float r, 
            float g, 
            float b)
        {
            A = 1.0f;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">Alpha component of the color.</param>
        /// <param name="r">Red component of the color.</param>
        /// <param name="g">Green component of the color.</param>
        /// <param name="b">Blue component of the color.</param>
        public Color(
            float a, 
            float r, 
            float g, 
            float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="r">Red component of the color.</param>
        /// <param name="g">Green component of the color.</param>
        /// <param name="b">Blue component of the color.</param>
        public Color(
            byte r, 
            byte g, 
            byte b)
        {
            A = 255;
            R = r.ArgbByteToFloat();
            G = g.ArgbByteToFloat();
            B = b.ArgbByteToFloat();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">Alpha component of the color.</param>
        /// <param name="r">Red component of the color.</param>
        /// <param name="g">Green component of the color.</param>
        /// <param name="b">Blue component of the color.</param>
        public Color(
            byte a, 
            byte r, 
            byte g, 
            byte b)
        {
            A = a.ArgbByteToFloat();
            R = r.ArgbByteToFloat();
            G = g.ArgbByteToFloat();
            B = b.ArgbByteToFloat();
        }

        /// <summary>
        /// Gets the alpha element of this color.
        /// </summary>
        public float A { get; }

        /// <summary>
        /// Gets the red element of this color.
        /// </summary>
        public float R { get; }

        /// <summary>
        /// Gets the green element of this color.
        /// </summary>
        public float G { get; }

        /// <summary>
        /// Gets the blue element of this color.
        /// </summary>
        public float B { get; }

        public static Color operator*(Color color, double scalar)
        {
            return
                new Color(
                    Math.Clamp(color.A * (float)scalar, 0.0f, 1.0f),
                    Math.Clamp(color.R * (float)scalar, 0.0f, 1.0f), 
                    Math.Clamp(color.G * (float)scalar, 0.0f, 1.0f),
                    Math.Clamp(color.B * (float)scalar, 0.0f, 1.0f)
                );
        }
    }
}
