namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of material.
    /// </summary>
    public class Material : IMaterial
    {
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        public ShadingType Shading
        {
            get => shading;
            set
            {
                shading = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        public event MaterialModifiedEventHandler? MaterialModified;

        private Color color;
        private ShadingType shading;
    }
}
