﻿using JSim.Core;
using JSim.Core.Common;
using JSim.Core.Render;
using System.Reflection;

namespace JSim.OpenTK
{
    public class ShaderManager
    {
        const string RES_ROOT = "JSim.OpenTK.Shaders.GLSL.";
        const string BASIC_VERT = "basicVertexShader.glsl";
        const string BASIC_FRAG = "basicFragmentShader.glsl";

        readonly BasicShader basicShader;

        public ShaderManager(ILogger logger)
        {
            string basicVert = LoadShaderFile(BASIC_VERT);
            string basicFrag = LoadShaderFile(BASIC_FRAG);

            basicShader =
                new BasicShader(
                    logger,
                    basicVert,
                    basicFrag
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
                //case ShadingType.Flat:
                //    return flatShader;
                //case ShadingType.Smooth:
                //    return smoothShader;
                default:
                    return basicShader;
            }
        }

        private static string LoadShaderFile(string name)
        {
            return
                EmbeddedResourceLoader.LoadEmbeddedFile(
                    RES_ROOT,
                    name,
                    Assembly.GetExecutingAssembly()
                );
        }
    }
}