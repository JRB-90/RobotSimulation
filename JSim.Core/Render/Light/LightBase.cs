using JSim.Core.Maths;

namespace JSim.Core.Render
{
    public abstract class LightBase : ILight
    {
        public LightBase(LightType lightType)
        {
            LightType = lightType;
            IsEnabled = true;
            Position = Vector3D.Origin;
            Direction = Vector3D.UnitZ;
            Color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Attenuation = new Attenuation();
            SpotCutoff = 0.0;
            SpotExponent = 0.0;
        }

        public LightType LightType { get; }

        public bool IsEnabled { get; set; }

        public Vector3D Position { get; set; }

        public Vector3D Direction { get; set; }

        public Color Color { get; set; }

        public Attenuation Attenuation { get; set; }

        public double SpotCutoff { get; set; }

        public double SpotExponent { get; set; }
    }
}
