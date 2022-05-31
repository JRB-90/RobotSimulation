namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of material.
    /// </summary>
    public class Material : IMaterial
    {
        const double DEFAULT_SHININESS = 0.0;
        const ShadingType DEFAULT_SHADING_TYPE = ShadingType.Solid;

        public Material()
        {
            ambient = GetDefaultColor();
            diffuse = GetDefaultColor();
            specular = GetDefaultColor();
            shininess = DEFAULT_SHININESS;
            shading = DEFAULT_SHADING_TYPE;
        }

        public Material(
            Color ambient,
            Color diffuse,
            Color specular)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            shininess = DEFAULT_SHININESS;
            shading = DEFAULT_SHADING_TYPE;
        }

        public Material(
            Color ambient,
            Color diffuse,
            Color specular,
            double shininess)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;
            shading = DEFAULT_SHADING_TYPE;
        }

        public Material(
            Color ambient,
            Color diffuse,
            Color specular,
            ShadingType shading)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = DEFAULT_SHININESS;
            this.shading = shading;
        }

        public Material(
            Color ambient,
            Color diffuse,
            Color specular,
            double shininess,
            ShadingType shading)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;
            this.shading = shading;
        }

        public Material(
            ITexture texture)
        {
            ambient = GetDefaultColor();
            diffuse = GetDefaultColor();
            specular = GetDefaultColor();
            this.texture = texture;
            this.shininess = DEFAULT_SHININESS;
            this.shading = DEFAULT_SHADING_TYPE;
        }

        public Material(
            ITexture texture,
            ShadingType shading)
        {
            ambient = GetDefaultColor();
            diffuse = GetDefaultColor();
            specular = GetDefaultColor();
            this.texture = texture;
            this.shininess = DEFAULT_SHININESS;
            this.shading = shading;
        }

        public Material(
            ITexture texture,
            double shininess,
            ShadingType shading)
        {
            ambient = GetDefaultColor();
            diffuse = GetDefaultColor();
            specular = GetDefaultColor();
            this.texture = texture;
            this.shininess = shininess;
            this.shading = shading;
        }

        /// <summary>
        /// Designates the ambient color component of the material.
        /// </summary>
        public Color Ambient
        {
            get => ambient;
            set
            {
                ambient = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        /// <summary>
        /// Designates the diffuse color component of the material.
        /// </summary>
        public Color Diffuse
        {
            get => diffuse;
            set
            {
                diffuse = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        /// <summary>
        /// Designates the specular color component of the material.
        /// </summary>
        public Color Specular
        {
            get => specular;
            set
            {
                specular = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        /// <summary>
        /// Designates how shiny the material is, i.e. how much light it reflects
        /// and how string the specular highlights are.
        /// </summary>
        public double Shininess
        {
            get => shininess;
            set
            {
                shininess = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        /// <summary>
        /// Designates the type of shading to apply.
        /// </summary>
        public ShadingType Shading
        {
            get => shading;
            set
            {
                shading = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        /// <summary>
        /// The texture to apply. Null if no texture needed.
        /// </summary>
        public ITexture? Texture
        {
            get => texture;
            set
            {
                texture = value;
                MaterialModified?.Invoke(this, new MaterialModifiedEventArgs());
            }
        }

        /// <summary>
        /// Event fired when the materials parameters have been altered.
        /// </summary>
        public event MaterialModifiedEventHandler? MaterialModified;

        public static Material FromSingleColor(Color color)
        {
            var ambient = color * 0.5;
            var diffuse = new Color(color);
            var specular = color * 1.5;

            return
                new Material(
                    ambient,
                    diffuse,
                    specular
                );
        }

        public static Material FromSingleColor(
            Color color,
            double shininess)
        {
            var ambient = color * 0.5;
            var diffuse = new Color(color);
            var specular = color * 1.5;

            return
                new Material(
                    ambient,
                    diffuse,
                    specular,
                    shininess
                );
        }

        public static Material FromSingleColor(
            Color color,
            ShadingType shading)
        {
            var ambient = color * 0.5;
            var diffuse = new Color(color);
            var specular = color * 1.5;

            return
                new Material(
                    ambient,
                    diffuse,
                    specular,
                    shading
                );
        }

        public static Material FromSingleColor(
            Color color,
            double shininess,
            ShadingType shading)
        {
            var ambient = color * 0.5;
            var diffuse = new Color(color);
            var specular = color * 1.5;

            return
                new Material(
                    ambient,
                    diffuse,
                    specular,
                    shininess,
                    shading
                );
        }

        private Color GetDefaultColor()
        {
            return new Color(1.0f, 0.0f, 0.0f, 0.0f);
        }

        private Color ambient;
        private Color diffuse;
        private Color specular;
        private double shininess;
        private ShadingType shading;
        private ITexture? texture;
    }
}
