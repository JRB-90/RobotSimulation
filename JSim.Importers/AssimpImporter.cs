using Assimp;
using Assimp.Configs;
using JSim.Core;
using JSim.Core.Importer;
using JSim.Core.SceneGraph;

namespace JSim.Importers
{
    public class AssimpImporter : IModelImporter
    {
        readonly ILogger logger;

        public AssimpImporter(ILogger logger)
        {
            this.logger = logger;
        }

        public ISceneEntity LoadModel(string path, ISceneAssembly parent)
        {
            var importer = new AssimpContext();

            var config = new NormalSmoothingAngleConfig(66.0f);
            importer.SetConfig(config);

            var supported = importer.GetSupportedImportFormats();

            var logStream =
                new LogStream(
                    delegate (String msg, String userData)
                    {
                        logger.Log(msg, LogLevel.Debug);
                    }
                );

            logStream.Attach();

            Assimp.Scene model = 
                importer.ImportFile(
                    path,
                    PostProcessSteps.FlipWindingOrder
                );

            ISceneEntity entity = parent.CreateNewEntity(Path.GetFileNameWithoutExtension(path));

            foreach (var mesh in model.Meshes)
            {
                int id = 0;

                var vertices =
                    mesh.Vertices
                    .Select(v =>
                    {
                        var vert = new Core.Render.Vertex(id, new Core.Maths.Vector3D(v.X, v.Y, v.Z));
                        id++;
                        return vert;
                    })
                    .ToArray();

                var indices = mesh.GetUnsignedIndices();

                var geo = entity.GeometryContainer.Root.CreateChildGeometry(mesh.Name);
                geo.SetDrawingData(vertices, indices);

                if (model.HasMaterials)
                {
                    geo.Material = ToJSimMaterial(model.Materials[mesh.MaterialIndex]);
                }
                else
                {
                    geo.Material.Color = new Core.Render.Color(1.0f, 0.5f, 0.5f, 0.5f);
                }
            }

            importer.Dispose();

            return entity;
        }

        private Core.Render.Material ToJSimMaterial(Material material)
        {
            var mat = new Core.Render.Material();
            mat.Shading = Core.Render.ShadingType.Flat;
            mat.Color = ToJSimColor(material.ColorDiffuse);

            return mat;
        }

        private Core.Render.Color ToJSimColor(Color4D color)
        {
            return
                new Core.Render.Color(
                    color.A,
                    color.R,
                    color.G,
                    color.B
                );
        }
    }
}