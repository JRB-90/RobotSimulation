namespace JSim.Core.Render
{
    /// <summary>
    /// Interface for all material types.
    /// </summary>
    public interface IMaterial
    {
        /// <summary>
        /// Designates the color to use.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Designates the type of shading to apply.
        /// </summary>
        ShadingType Shading { get; set; }

        /// <summary>
        /// Event fired when the materials parameters have been altered.
        /// </summary>
        event MaterialModifiedEventHandler? MaterialModified;
    }
}
