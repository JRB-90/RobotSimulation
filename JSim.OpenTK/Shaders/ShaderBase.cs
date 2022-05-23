using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace JSim.OpenTK
{
    /// <summary>
    /// Abstract base class for all opengl shaders.
    /// </summary>
    internal abstract class ShaderBase : IShader
    {
        readonly ILogger logger;

        public ShaderBase(
            ILogger logger,
            string vsource,
            string fsource)
        {
            this.logger = logger;
            uniforms = new Dictionary<string, int>();
            CompileProgram(vsource, fsource);
        }

        public void Dispose()
        {
            GL.DeleteProgram(program);
        }

        /// <summary>
        /// Binds the shader to the current opengl context.
        /// </summary>
        public void Bind()
        {
            GL.UseProgram(program);
        }

        /// <summary>
        /// Unbinds the shader from the current opengl context.
        /// </summary>
        public void Unbind()
        {
            GL.UseProgram(0);
        }

        /// <summary>
        /// When overriding, all uniforms in a shader must be updated.
        /// </summary>
        /// <param name="model">Model matrix.</param>
        /// <param name="camera">Camera.</param>
        /// <param name="material">Material data.</param>
        public abstract void UpdateUniforms(
            Transform3D model, 
            ICamera camera, 
            IMaterial material,
            SceneLighting sceneLighting
        );

        /// <summary>
        /// Adds a glsl uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        protected void AddUniform(string name)
        {
            int uniformID = GL.GetUniformLocation(program, name);
            if (uniformID == -1)
            {
                //logger.Log(
                //    $"Error adding uniform {name} : Location could not be found",
                //    LogLevel.Error
                //);

                //return;

                throw
                    new InvalidOperationException(
                        $"Error adding uniform {name} : Location could not be found"
                    );
            }
            uniforms.Add(name, uniformID);
        }

        /// <summary>
        /// Sets an int uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformInt(string name, int value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                GL.Uniform1(location, value);
                GL.EnableVertexAttribArray(location);
            }
            else
            {
                throw new ArgumentException($"Failed to set uniform: {name}");
            }
        }

        /// <summary>
        /// Sets a float uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformFloat(string name, float value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                GL.Uniform1(location, value);
                GL.EnableVertexAttribArray(location);
            }
            else
            {
                throw new ArgumentException($"Failed to set uniform: {name}");
            }
        }

        /// <summary>
        /// Sets a 2 dimensional vector uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformVec2(string name, Vector2D value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                GL.Uniform2(
                    location, 
                    (float)value.X,
                    (float)value.Y
                );

                GL.EnableVertexAttribArray(location);
            }
            else
            {
                throw new ArgumentException($"Failed to set uniform: {name}");
            }
        }

        /// <summary>
        /// Sets a 3 dimensional vector uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformVec3(string name, Vector3D value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                GL.Uniform3(
                    location, 
                    (float)value.X,
                    (float)value.Y, 
                    (float)value.Z
                );

                GL.EnableVertexAttribArray(location);
            }
            else
            {
                throw new ArgumentException($"Failed to set uniform: {name}");
            }
        }

        /// <summary>
        /// Sets a Color uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformColor(string name, Color value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                GL.Uniform4(
                    location,
                    value.R,
                    value.G,
                    value.B,
                    value.A
                );

                GL.EnableVertexAttribArray(location);
            }
            else
            {
                throw new ArgumentException($"Failed to set uniform: {name}");
            }
        }

        /// <summary>
        /// Sets a transform matrix uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformMatrix4x4(string name, Transform3D value)
        {
            SetUniformMatrix4x4(name, value.Matrix);
        }

        /// <summary>
        /// Sets an equal sided matrix uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformMatrix4x4(string name, MathNet.Numerics.LinearAlgebra.Matrix<double> value)
        {
            if (value.ColumnCount != 4 ||
                value.RowCount != 4)
            {
                throw new ArgumentException("Matrix in incorrect format, should be a 4x4");
            }

            if (uniforms.TryGetValue(name, out int location))
            {
                Matrix4 mat =
                    new Matrix4(
                        (float)value[0, 0], (float)value[0, 1], (float)value[0, 2], (float)value[0, 3],
                        (float)value[1, 0], (float)value[1, 1], (float)value[1, 2], (float)value[1, 3],
                        (float)value[2, 0], (float)value[2, 1], (float)value[2, 2], (float)value[2, 3],
                        (float)value[3, 0], (float)value[3, 1], (float)value[3, 2], (float)value[3, 3]
                    );

                GL.UniformMatrix4(location, true, ref mat);
                GL.EnableVertexAttribArray(location);
            }
            else
            {
                throw new ArgumentException($"Failed to set uniform: {name}");
            }
        }

        protected int ToLightTypeInt(LightType lightType)
        {
            switch (lightType)
            {
                case LightType.Directional:
                    return 1;
                case LightType.Point:
                    return 2;
                case LightType.Spot:
                    return 3;
                default:
                    return 0;
            }
        }

        private void CompileProgram(
            string vsource,
            string fsource)
        {
            program = GL.CreateProgram();

            CompileShader(
                vsource,
                ShaderType.VertexShader
            );

            CompileShader(
                fsource,
                ShaderType.FragmentShader
            );

            GL.LinkProgram(program);
            GL.GetProgram(
                program, 
                GetProgramParameterName.LinkStatus, 
                out int linkStatus
            );

            if (linkStatus == 0)
            {
                logger.Log(
                    $"Failed to link shader program. Status Code: {linkStatus}",
                    LogLevel.Error
                );

                logger.Log(GL.GetProgramInfoLog(program), LogLevel.Error);

                throw new ArgumentException("Failed to load shader, failed to link");
            }
        }

        private void CompileShader(
            string source,
            ShaderType shaderType)
        {
            int shaderID = GL.CreateShader(shaderType);

            GL.ShaderSource(shaderID, source);
            GL.CompileShader(shaderID);
            GL.GetShader(
                shaderID,
                ShaderParameter.CompileStatus,
                out int statusCode
            );

            if (statusCode != 1)
            {
                GL.GetShaderInfoLog(shaderID, out string info);
                GL.DeleteShader(shaderID);
                GL.DeleteProgram(program);

                throw new ArgumentException($"Failed to compile shader: {info}");
            }

            GL.AttachShader(program, shaderID);
            GL.DeleteShader(shaderID);
        }

        private int program;
        private Dictionary<string, int> uniforms;
    }
}
