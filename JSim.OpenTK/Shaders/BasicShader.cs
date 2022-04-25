using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    ///  Basic shader that shaders a solid color, with no shading.
    /// </summary>
    internal class BasicShader : ShaderBase
    {
        public BasicShader(
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
            AddUniform("newColor");
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
                camera.CameraProjection.GetProjectionMatrix() *
                camera.CameraProjection.GetViewMatrix(camera) *
                model.Matrix
            );

            SetUniformVec4(
                "newColor",
                new Color(255, 0, 0, 0)
                //material.Diffuse
            );
        }
    }
}
