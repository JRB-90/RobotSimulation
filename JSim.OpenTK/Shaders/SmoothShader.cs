using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.OpenTK
{
    /// <summary>
    /// Provides smooth phong shading across all faces.
    /// </summary>
    internal class SmoothShader : ShaderBase
    {
        public SmoothShader(
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
            AddUniform("activeLights");
            AddUniform("ambientLight");

            for (int i = 0; i < OpenTKRenderingEngine.MAX_LIGHTS; i++)
            {
                AddLightSourceUniform($"lights[{i}]");
            }

            AddUniform("material.ambient");
            //AddUniform("material.diffuse");
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

            SetUniformInt(
                "activeLights",
                sceneLighting.Lights.Count
            );

            SetUniformColor(
                "ambientLight",
                sceneLighting.AmbientLight.Color
            );

            for (int i = 0; i < sceneLighting.Lights.Count; i++)
            {
                if (i < OpenTKRenderingEngine.MAX_LIGHTS)
                {
                    SetLightUniforms($"lights[{i}]", sceneLighting.Lights[i]);
                }
            }

            SetUniformColor(
                "material.ambient",
                material.Ambient
            );

            //SetUniformColor(
            //    "material.diffuse",
            //    material.Diffuse
            //);
        }

        private void AddLightSourceUniform(string name)
        {
            AddUniform(name + ".type");
            AddUniform(name + ".color");
            //AddUniform(name + ".direction");
            //AddUniform(name + ".constantAttenuation");
            //AddUniform(name + ".linearAttenuation");
            //AddUniform(name + ".quadraticAttenuation");
            //AddUniform(name + ".spotCutoff");
            //AddUniform(name + ".spotExponent");
        }

        private void SetLightUniforms(
            string name,
            ILight light)
        {
            SetUniformInt(name + ".type", ToLightTypeInt(light.LightType));
            SetUniformColor(name + ".color", light.Color);
            //SetUniformVec3(name + ".direction", light.Direction);
            //SetUniformFloat(name + ".constantAttenuation", (float)light.Attenuation.Constant);
            //SetUniformFloat(name + ".linearAttenuation", (float)light.Attenuation.Linear);
            //SetUniformFloat(name + ".quadraticAttenuation", (float)light.Attenuation.Quadratic);
            //SetUniformFloat(name + ".spotCutoff", (float)light.SpotCutoff);
            //SetUniformFloat(name + ".spotExponent", (float)light.SpotExponent);
        }
    }
}
