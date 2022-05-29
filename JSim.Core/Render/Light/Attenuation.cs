namespace JSim.Core.Render
{
    /// <summary>
    /// Contains a quadratic polynomial function that represents a lights
    /// decay over distance from the source.
    /// </summary>
    public class Attenuation
    {
        public Attenuation()
        {
            Constant = 1.0;
            Linear = 0.0;
            Quadratic = 0.0;
        }

        public Attenuation(
            double constant,
            double linear,
            double quadratic)
        {
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
        }

        /// <summary>
        /// Gets the constant attenuation component.
        /// </summary>
        public double Constant { get; set; }

        /// <summary>
        /// Gets the linear attenuation component.
        /// </summary>
        public double Linear { get; set; }

        /// <summary>
        /// Gets the quadratic attenuation component.
        /// </summary>
        public double Quadratic { get; set; }
    }
}
