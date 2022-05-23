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
            AddUniform("ambientLight");
            AddUniform("light.color");
            AddUniform("light.direction");
            AddUniform("material.ambient");
            AddUniform("material.diffuse");
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
                "modelMat",
                model
            );

            SetUniformMatrix4x4(
                "mvpMat",
                mvp
            );

            SetUniformColor(
                "ambientLight",
                sceneLighting.AmbientLight.Color
            );

            var dir =
                sceneLighting.Lights
                .OfType<DirectionalLight>()
                .FirstOrDefault();

            if (dir != null)
            {
                SetUniformColor(
                        "light.color",
                        dir.Color
                    );

                SetUniformVec3(
                    "light.direction",
                    dir.Direction
                );
            }
            else
            {
                SetUniformColor(
                        "light.color",
                        new Color(0.0f, 0.0f, 0.0f, 0.0f)
                    );

                SetUniformVec3(
                    "light.direction",
                    new Vector3D(0, 0, 0)
                );
            }

            SetUniformColor(
                "material.ambient",
                material.Ambient
            );

            SetUniformColor(
                "material.diffuse", 
                material.Diffuse
            );
        }
    }
}
