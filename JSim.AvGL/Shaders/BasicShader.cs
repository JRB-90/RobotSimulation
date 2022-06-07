using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.AvGL
{
    /// <summary>
    ///  Basic shader that shaders a solid color, with no shading.
    /// </summary>
    internal class BasicShader : ShaderBase
    {
        public BasicShader(
            ILogger logger,
            GLVersion glVersion,
            GLBindingsInterface gl,
            string vsource, 
            string fsource) 
          : 
            base(
                logger,
                glVersion,
                gl,
                vsource,
                fsource)
        {
            AddUniform("mvpMat");
            AddUniform("modelColor");
        }

        public override void UpdateUniforms(
            Transform3D model, 
            ICamera camera, 
            IMaterial material,
            SceneLighting sceneLighting)
        {
            Matrix<double> mvp =
                camera.GetProjectionMatrix() *
                camera.GetViewMatrix() *
                model.Matrix;

            SetUniformMatrix4x4(
                "mvpMat",
                mvp
            );

            SetUniformColor(
                "modelColor",
                material.Diffuse
            );
        }
    }
}
