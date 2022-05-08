using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// Provides flat shading across all faces.
    /// </summary>
    internal class FlatShader : ShaderBase
    {
        public FlatShader(
            ILogger logger,
            string vsource,
            string fsource)
          :
            base(
                logger,
                vsource,
                fsource)
        {
            AddUniform("modelMat");
            AddUniform("MVPMat");
            AddUniform("light.color");
            AddUniform("light.direction");
            AddUniform("material.ambient");
            AddUniform("material.diffuse");
        }

        public override void UpdateUniforms(
            Transform3D model, 
            ICamera camera, 
            IMaterial material)
        {
            SetUniformMatrix(
                "modelMat",
                model
            );

            SetUniformMatrix(
                "MVPMat",
                camera.GetProjectionMatrix() *
                camera.GetViewMatrix() *
                model.Matrix
            );

            SetUniformVec4("light.color", new Color(1.0f, 1.0f, 1.0f, 1.0f));
            SetUniformVec3("light.direction", new Vector3D(1, 1, 1));

            SetUniformVec4("material.ambient", new Color(1.0f, 0.2f, 0.2f, 0.2f));
            SetUniformVec4("material.diffuse", material.Color);
        }
    }
}
