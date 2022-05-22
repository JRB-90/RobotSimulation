using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using MathNet.Numerics.LinearAlgebra;

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
            AddUniform("mvpMat");
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
            Matrix<double> mvp =
                camera.GetProjectionMatrix() *
                camera.GetViewMatrix() *
                model.Matrix;

            SetUniformMatrix4x4(
                "modelMat",
                model
            );

            SetUniformMatrix4x4(
                "mvpMat",
                mvp
            );

            SetUniformColor(
                "light.color", 
                new Color(1.0f, 1.0f, 1.0f, 1.0f)
            );

            SetUniformVec3(
                "light.direction", 
                new Vector3D(-1, -1, -1)
            );

            SetUniformColor(
                "material.ambient", 
                new Color(1.0f, 0.1f, 0.1f, 0.1f)
            );

            SetUniformColor(
                "material.diffuse", 
                material.Color
            );
        }
    }
}
