namespace JSim.Core.Render
{
    /// <summary>
    /// Interface for all material types.
    /// </summary>
    public interface IMaterial
    {
        /// <summary>
        /// Designates the ambient color component of the material.
        /// </summary>
        Color Ambient { get; set; }

        /// <summary>
        /// Designates the diffuse color component of the material.
        /// </summary>
        Color Diffuse { get; set; }

        /// <summary>
        /// Designates the specular color component of the material.
        /// </summary>
        Color Specular { get; set; }

        /// <summary>
        /// Designates how shiny the material is, i.e. how much light it reflects
        /// and how string the specular highlights are.
        /// </summary>
        double Shininess { get; set; }

        /// <summary>
        /// Designates the type of shading to apply.
        /// </summary>
        ShadingType Shading { get; set; }

        /// <summary>
        /// The texture to apply. Null if no texture needed.
        /// </summary>
        ITexture? Texture { get; set; }

        /// <summary>
        /// Event fired when the materials parameters have been altered.
        /// </summary>
        event MaterialModifiedEventHandler? MaterialModified;
    }
}
