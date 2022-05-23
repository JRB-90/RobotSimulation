using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Represents a light that projects out in a cone.
    /// </summary>
    public class SpotLight : LightBase
    {
        public SpotLight(
            Vector3D position,
            Vector3D direction)
          :
            base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            SpotCutoff = 45.0;
            SpotExponent = 1.0;
        }

        public SpotLight(
            Vector3D position,
            Vector3D direction,
            Color color)
          :
            base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            Color = color;
            SpotCutoff = 45.0;
            SpotExponent = 1.0;
        }

        public SpotLight(
            Vector3D position,
            Vector3D direction,
            Attenuation attenuation)
          :
            base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            Attenuation = attenuation;
            SpotCutoff = 45.0;
            SpotExponent = 1.0;
        }

        public SpotLight(
            Vector3D position,
            Vector3D direction,
            double spotCutoff,
            double spotExponent)
          :
            base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            SpotCutoff = spotCutoff;
            SpotExponent = spotExponent;
        }

        public SpotLight(
            Vector3D position,
            Vector3D direction,
            Color color,
            double spotCutoff,
            double spotExponent)
          :
            base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            Color = color;
            SpotCutoff = spotCutoff;
            SpotExponent = spotExponent;
        }

        public SpotLight(
            Vector3D position,
            Vector3D direction,
            Attenuation attenuation,
            double spotCutoff,
            double spotExponent)
         :
           base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            Attenuation = attenuation;
            SpotCutoff = spotCutoff;
            SpotExponent = spotExponent;
        }

        public SpotLight(
            Vector3D position,
            Vector3D direction,
            Color color,
            Attenuation attenuation,
            double spotCutoff,
            double spotExponent)
         :
           base(LightType.Spot)
        {
            Position = position;
            Direction = direction;
            Color = color;
            Attenuation = attenuation;
            SpotCutoff = spotCutoff;
            SpotExponent = spotExponent;
        }
    }
}
