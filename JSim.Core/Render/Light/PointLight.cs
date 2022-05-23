using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Represents a light that emits in all directions.
    /// </summary>
    public class PointLight : LightBase
    {
        public PointLight(Vector3D position)
          :
            base(LightType.Point)
        {
            Position = position;
            SpotCutoff = 360.0;
            SpotExponent = 0.0;
        }

        public PointLight(
            Vector3D position,
            Color color)
          :
            base(LightType.Point)
        {
            Position = position;
            Color = color;
            SpotCutoff = 360.0;
            SpotExponent = 0.0;
        }

        public PointLight(
            Vector3D position,
            Attenuation attenuation)
          :
            base(LightType.Point)
        {
            Position = position;
            Attenuation = attenuation;
            SpotCutoff = 360.0;
            SpotExponent = 0.0;
        }

        public PointLight(
            Vector3D position,
            Color color,
            Attenuation attenuation)
          :
            base(LightType.Point)
        {
            Position = position;
            Color = color;
            Attenuation = attenuation;
            SpotCutoff = 360.0;
            SpotExponent = 0.0;
        }
    }
}
