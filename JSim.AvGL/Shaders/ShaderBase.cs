using JSim.Core;
using JSim.Core.Maths;
using JSim.Core.Render;
using System.Numerics;
using System.Runtime.InteropServices;
using static Avalonia.OpenGL.GlConsts;

namespace JSim.AvGL
{
    /// <summary>
    /// Abstract base class for all opengl shaders.
    /// </summary>
    internal abstract class ShaderBase : IShader
    {
        readonly ILogger logger;

        public ShaderBase(
            ILogger logger,
            GLVersion glVersion,
            GLBindingsInterface gl,
            string vsource,
            string fsource)
        {
            this.logger = logger;
            this.gLVersion = glVersion;
            this.gl = gl;
            uniforms = new Dictionary<string, int>();
            CompileProgram(vsource, fsource);
        }

        public void Dispose()
        {
            gl.DeleteProgram(program);
        }

        /// <summary>
        /// Binds the shader to the current opengl context.
        /// </summary>
        public void Bind()
        {
            gl.UseProgram(program);
        }

        /// <summary>
        /// Unbinds the shader from the current opengl context.
        /// </summary>
        public void Unbind()
        {
            gl.UseProgram(0);
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
            int uniformID = gl.GetUniformLocationString(program, name);

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
                gl.Uniform1i(location, value);
                gl.EnableVertexAttribArray(location);
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
                gl.Uniform1f(location, value);
                gl.EnableVertexAttribArray(location);
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
                gl.Uniform2f(
                    location,
                    (float)value.X,
                    (float)value.Y
                );

                gl.EnableVertexAttribArray(location);
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
                gl.Uniform3f(
                    location,
                    (float)value.X,
                    (float)value.Y,
                    (float)value.Z
                );

                gl.EnableVertexAttribArray(location);
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
                gl.Uniform4f(
                    location,
                    value.R,
                    value.G,
                    value.B,
                    value.A
                );

                gl.EnableVertexAttribArray(location);
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
        protected unsafe void SetUniformMatrix4x4(string name, MathNet.Numerics.LinearAlgebra.Matrix<double> value)
        {
            if (value.ColumnCount != 4 ||
                value.RowCount != 4)
            {
                throw new ArgumentException("Matrix in incorrect format, should be a 4x4");
            }

            if (uniforms.TryGetValue(name, out int location))
            {
                Matrix4x4 mat =
                    new Matrix4x4(
                        (float)value[0, 0], (float)value[0, 1], (float)value[0, 2], (float)value[0, 3],
                        (float)value[1, 0], (float)value[1, 1], (float)value[1, 2], (float)value[1, 3],
                        (float)value[2, 0], (float)value[2, 1], (float)value[2, 2], (float)value[2, 3],
                        (float)value[3, 0], (float)value[3, 1], (float)value[3, 2], (float)value[3, 3]
                    );

                gl.UniformMatrix4fv(location, 1, true, &mat);
                gl.EnableVertexAttribArray(location);
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
            program = gl.CreateProgram();

            CompileShader(
                vsource,
                GL_VERTEX_SHADER
            );

            CompileShader(
                fsource,
                GL_FRAGMENT_SHADER
            );

            if (!gl.LinkProgramAndGetError(program, out string errorString))
            {
                logger.Log(
                    $"Failed to link shader program: {errorString}",
                    LogLevel.Error
                );

                throw new ArgumentException("Failed to load shader, failed to link");
            }
        }

        private void CompileShader(
            string source,
            int shaderType)
        {
            int shaderID = gl.CreateShader(shaderType);

            if (!gl.CompileShaderAndGetError(shaderID, source, out string errorString))
            {
                logger.Log(
                    $"Failed to compile shader: {errorString}",
                    LogLevel.Error
                );

                throw new ArgumentException("Failed to load shader, failed to compile");
            }

            gl.AttachShader(program, shaderID);
            gl.DeleteShader(shaderID);
        }

        private int program;
        private GLVersion gLVersion;
        private Dictionary<string, int> uniforms;
        private GLBindingsInterface gl;
    }
}
