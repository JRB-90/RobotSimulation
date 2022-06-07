using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Render;
using System.Reflection;

namespace JSim.AvGL
{
    public class ShaderManager
    {
        const string RES_ROOT = "JSim.AvGL.Shaders.GLSL.";
        const string BASIC_VS = "basicVS.glsl";
        const string BASIC_FS = "basicFS.glsl";
        const string ADVANCED_FS = "advancedFS.glsl";

        //readonly BasicShader basicShader;
        //readonly AdvancedShader flatShader;
        //readonly AdvancedShader smoothShader;

        public ShaderManager(
            ILogger logger,
            GLBindingsInterface gl,
            GLVersion gLVersion)
        {
            GLVersion targetVersion;
            if (gLVersion <= new GLVersion(3, 0))
            {
                targetVersion = new GLVersion(1, 2);
            }
            else
            {
                targetVersion = new GLVersion(3, 3);
            }

            //string basicVS =
            //    ProcessShader(
            //        LoadShaderFile(BASIC_VS),
            //        ShaderType.VertexShader,
            //        targetVersion
            //    );

            //string basicFS =
            //    ProcessShader(
            //        LoadShaderFile(BASIC_FS),
            //        ShaderType.FragmentShader,
            //        targetVersion
            //    );

            //string advancedFS =
            //    ProcessShader(
            //        LoadShaderFile(ADVANCED_FS),
            //        ShaderType.FragmentShader,
            //        targetVersion
            //    );

            //basicShader =
            //    new BasicShader(
            //        logger,
            //        gLVersion,
            //        basicVS,
            //        basicFS
            //    );

            //flatShader =
            //    new AdvancedShader(
            //        logger,
            //        gLVersion,
            //        basicVS,
            //        advancedFS,
            //        true
            //    );

            //smoothShader =
            //    new AdvancedShader(
            //        logger,
            //        gLVersion,
            //        basicVS,
            //        advancedFS,
            //        false
            //    );
        }

        internal IShader FindShader(ShadingType shadingType)
        {
            switch (shadingType)
            {
                //case ShadingType.Wire:
                //    return wireShader;
                //case ShadingType.Solid:
                //    return basicShader;
                //case ShadingType.Flat:
                //    return flatShader;
                //case ShadingType.Smooth:
                //    return smoothShader;
                //default:
                //    return basicShader;
            }

            throw new NotImplementedException();
        }

        private string LoadShaderFile(string name)
        {
            return
                EmbeddedResourceLoader.LoadEmbeddedFile(
                    RES_ROOT,
                    name,
                    Assembly.GetExecutingAssembly()
                );
        }

        private string ProcessShader(
            string shaderSource, 
            ShaderType shaderType,
            GLVersion targetVersion)
        {
            string[] lines = shaderSource.Split(Environment.NewLine);

            int locationCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("#version"))
                {
                    lines[i] = ProcessVersion(lines[i], targetVersion);
                }
                else if (lines[i].Contains("attribute"))
                {
                    lines[i] = ProcessAttribute(lines[i], targetVersion, locationCount);
                    locationCount++;
                }
                else if (lines[i].Contains("layout"))
                {
                    lines[i] = ProcessLayout(lines[i], targetVersion, locationCount);
                    locationCount++;
                }
                else if (lines[i].Contains("varying"))
                {
                    lines[i] = ProcessVarying(lines[i], targetVersion, shaderType);
                }
                else if (lines[i].Contains("out"))
                {
                    lines[i] = ProcessOut(lines[i], targetVersion);
                }
                else if (lines[i].Contains("//FRAG_OUT"))
                {
                    lines[i] = ProcessFragOut(lines[i], targetVersion);
                }
                else if (lines[i].Contains("gl_FragColor"))
                {
                    lines[i] = ProcessGlFragColor(lines[i], targetVersion);
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

        private string ProcessVersion(
            string line,
            GLVersion targetVersion)
        {
            return $"#version {targetVersion.Major}{targetVersion.Minor}0";
        }

        private string ProcessAttribute(
            string line,
            GLVersion targetVersion,
            int locationCount)
        {
            if (targetVersion <= new GLVersion(1, 2))
            {
                return line;
            }
            else
            {
                return line.Replace("attribute", $"layout (location = {locationCount}) in");
            }
        }

        private string ProcessLayout(
            string line,
            GLVersion targetVersion,
            int locationCount)
        {
            if (targetVersion > new GLVersion(1, 2))
            {
                return line;
            }
            else
            {
                int start = -1;
                int end = -1;

                start = line.IndexOf("layout");
                end = line.IndexOf("in") + 2;
                
                var trimmedString = line.Remove(start, end - start);

                return "attribute" + trimmedString;
            }
        }

        private string ProcessVarying(
            string line,
            GLVersion targetVersion,
            ShaderType shaderType)
        {
            if (targetVersion <= new GLVersion(1, 2))
            {
                return line;
            }
            else
            {
                if (shaderType == ShaderType.VertexShader)
                {
                    return line.Replace("varying", "out");
                }
                else
                {
                    return line.Replace("varying", "in");
                }
            }
        }

        private string ProcessOut(
            string line,
            GLVersion targetVersion)
        {
            if (targetVersion > new GLVersion(1, 2))
            {
                return line;
            }
            else
            {
                return line.Replace("out", "varying");
            }
        }

        private string ProcessFragOut(
            string line,
            GLVersion targetVersion)
        {
            if (targetVersion <= new GLVersion(1, 2))
            {
                return line;
            }
            else
            {
                return "out vec4 fragColor;";
            }
        }

        private string ProcessGlFragColor(
            string line,
            GLVersion targetVersion)
        {
            if (targetVersion <= new GLVersion(1, 2))
            {
                return line;
            }
            else
            {
                return line.Replace("gl_FragColor", "fragColor");
            }
        }
    }
}
