using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Represents the behaviour of all light sources in the 3D environment.
    /// </summary>
    public interface ILight
    {
        /// <summary>
        /// Gets the type of light.
        /// </summary>
        LightType LightType { get; }

        /// <summary>
        /// Enables and disabled the light.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Position of the light in the world.
        /// </summary>
        Vector3D Position { get; set; }

        /// <summary>
        /// Direction the light is pointing.
        /// </summary>
        Vector3D Direction { get; set; }

        /// <summary>
        /// Color of the light.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Attenuation profile of the emitted light.
        /// </summary>
        Attenuation Attenuation { get; set; }

        /// <summary>
        /// Size of the spot light cutoff.
        /// </summary>
        double SpotCutoff { get; set; }

        /// <summary>
        /// The exponention rolloff of the spot as it approaches the cutoff.
        /// </summary>
        double SpotExponent { get; set; }
    }
}
