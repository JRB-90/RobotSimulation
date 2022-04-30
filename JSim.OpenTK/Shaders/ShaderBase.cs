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
            IMaterial material
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
                logger.Log(
                    $"Error adding uniform {name} : Location could not be found",
                    LogLevel.Error
                );

                return;
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
        }

        /// <summary>
        /// Sets a 4 dimensional vector uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformVec4(string name, Color value)
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
        }

        /// <summary>
        /// Sets a rotation matrix uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformMatrix(string name, Rotation3D value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                Matrix3 mat = ToOpenTKMat3(value);
                GL.UniformMatrix3(location, false, ref mat);
                GL.EnableVertexAttribArray(location);
            }
        }

        /// <summary>
        /// Sets a transform matrix uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformMatrix(string name, Transform3D value)
        {
            if (uniforms.TryGetValue(name, out int location))
            {
                Matrix4 mat = ToOpenTKMat4(value);
                GL.UniformMatrix4(location, false, ref mat);
                GL.EnableVertexAttribArray(location);
            }
        }

        /// <summary>
        /// Sets an equal sided matrix uniform.
        /// </summary>
        /// <param name="name">Name of the uniform.</param>
        /// <param name="value">Value of the uniform.</param>
        protected void SetUniformMatrix(string name, MathNet.Numerics.LinearAlgebra.Matrix<double> value)
        {
            if (value.ColumnCount != 4 ||
                value.RowCount != 4)
            {
                throw new ArgumentException("Matrix in incorrect format, should be a 4x4");
            }

            if (uniforms.TryGetValue(name, out int location))
            {
                Matrix4 mat = ToOpenTKMat4(value);
                GL.UniformMatrix4(location, false, ref mat);
                GL.EnableVertexAttribArray(location);
            }
        }

        private Matrix3 ToOpenTKMat3(Rotation3D rotation)
        {
            Matrix3 res = new Matrix3();

            res.M11 = (float)rotation.Matrix[0, 0]; 
            res.M21 = (float)rotation.Matrix[0, 1]; 
            res.M31 = (float)rotation.Matrix[0, 2]; 
            res.M12 = (float)rotation.Matrix[1, 0]; 
            res.M22 = (float)rotation.Matrix[1, 1]; 
            res.M32 = (float)rotation.Matrix[1, 2]; 
            res.M13 = (float)rotation.Matrix[2, 0];
            res.M23 = (float)rotation.Matrix[2, 1];
            res.M33 = (float)rotation.Matrix[2, 2];

            return res;
        }

        private Matrix4 ToOpenTKMat4(Transform3D transform)
        {
            return ToOpenTKMat4(transform.Matrix);
        }

        private Matrix4 ToOpenTKMat4(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix)
        {
            Matrix4 res = new Matrix4();

            res.M11 = (float)matrix[0, 0];
            res.M21 = (float)matrix[0, 1];
            res.M31 = (float)matrix[0, 2];
            res.M41 = (float)matrix[0, 3];
            res.M12 = (float)matrix[1, 0];
            res.M22 = (float)matrix[1, 1];
            res.M32 = (float)matrix[1, 2];
            res.M42 = (float)matrix[1, 3];
            res.M13 = (float)matrix[2, 0];
            res.M23 = (float)matrix[2, 1];
            res.M33 = (float)matrix[2, 2];
            res.M43 = (float)matrix[2, 3];
            res.M14 = (float)matrix[3, 0];
            res.M24 = (float)matrix[3, 1];
            res.M34 = (float)matrix[3, 2];
            res.M44 = (float)matrix[3, 3];

            return res;
        }

        private Vector4 ToOpenTKVec4(Color color)
        {
            return 
                new Vector4(
                    color.R, 
                    color.G,
                    color.B, 
                    color.A
                );
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
                GL.DeleteShader(shaderID);
                GL.DeleteProgram(program);

                throw new ArgumentException($"Failed to compile shader: {GL.GetError()}");
            }

            GL.AttachShader(program, shaderID);
            GL.DeleteShader(shaderID);
        }

        private int program;
        private Dictionary<string, int> uniforms;
    }
}
