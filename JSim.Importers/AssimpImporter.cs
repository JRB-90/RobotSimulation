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
                    PostProcessSteps.GenerateNormals |
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
                geo.Material.Shading = Core.Render.ShadingType.Flat;
                geo.Material.Color = new Core.Render.Color(1.0f, 0.5f, 0.5f, 0.5f);
            }

            importer.Dispose();

            return entity;
        }
    }
}