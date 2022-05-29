using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Render;
using System.Reflection;

namespace JSim.OpenTK
{
    public class ShaderManager
    {
        const string RES_ROOT = "JSim.OpenTK.Shaders.GLSL.";
        const string BASIC_VERT = "basicVertex.glsl";
        const string BASIC_FRAG = "basicFragment.glsl";
        const string FLAT_FRAG = "flatFragment.glsl";
        const string SMOOTH_FRAG = "smoothFragment.glsl";

        readonly string versionFolder;

        readonly BasicShader basicShader;
        readonly FlatShader flatShader;
        readonly SmoothShader smoothShader;

        public ShaderManager(
            ILogger logger,
            GLVersion gLVersion)
        {
            if (gLVersion <= new GLVersion(3, 0))
            {
                versionFolder = "V120.";
            }
            else
            {
                versionFolder = "V330.";
            }

            string basicVert = LoadShaderFile(BASIC_VERT);
            string basicFrag = LoadShaderFile(BASIC_FRAG);
            string flatFrag = LoadShaderFile(FLAT_FRAG);
            string smoothFrag = LoadShaderFile(SMOOTH_FRAG);

            basicShader =
                new BasicShader(
                    logger,
                    gLVersion,
                    basicVert,
                    basicFrag
                );

            flatShader =
                new FlatShader(
                    logger,
                    gLVersion,
                    basicVert,
                    flatFrag
                );

            smoothShader =
                new SmoothShader(
                    logger,
                    gLVersion,
                    basicVert,
                    smoothFrag
                );
        }

        internal IShader FindShader(ShadingType shadingType)
        {
            switch (shadingType)
            {
                //case ShadingType.Wire:
                //    return wireShader;
                case ShadingType.Solid:
                    return basicShader;
                case ShadingType.Flat:
                    return flatShader;
                case ShadingType.Smooth:
                    return smoothShader;
                default:
                    return basicShader;
            }
        }

        private string LoadShaderFile(string name)
        {
            return
                EmbeddedResourceLoader.LoadEmbeddedFile(
                    RES_ROOT + versionFolder,
                    name,
                    Assembly.GetExecutingAssembly()
                );
        }
    }
}
