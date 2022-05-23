namespace JSim.Core.Render
{
    /// <summary>
    /// Represents the ambient background lighting which is consistentant everywhere in the scene.
    /// </summary>
    public class AmbientLight
    {
        public AmbientLight()
        {
            Color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public AmbientLight(Color color)
        {
            Color = color;
        }

        public Color Color { get; set; }
    }
}
