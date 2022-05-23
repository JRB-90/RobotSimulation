using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Represents a light in one direction that is conistent across the whole scene.
    /// </summary>
    public class DirectionalLight : LightBase
    {
        public DirectionalLight(Vector3D direction)
          :
            base(LightType.Directional)
        {
            Direction = direction;
        }

        public DirectionalLight(
            Vector3D direction,
            Color color)
          :
            base(LightType.Directional)
        {
            Direction = direction;
            Color = color;
        }

        public DirectionalLight(
            Vector3D direction,
            Color color,
            Attenuation attenuation)
          :
            base(LightType.Directional)
        {
            Direction = direction;
            Color = color;
            Attenuation = attenuation;
        }
    }
}
