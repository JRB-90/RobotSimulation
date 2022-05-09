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

        public ISceneObject LoadModel(string path, ISceneAssembly parent)
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

            ISceneAssembly assembly = parent.CreateNewAssembly(Path.GetFileNameWithoutExtension(path));
            ProcessNode(model, model.RootNode, assembly);
            importer.Dispose();

            return assembly;
        }

        private void ProcessNode(
            Assimp.Scene model, 
            Node node, 
            ISceneAssembly assembly)
        {
            foreach (var childNode in node.Children)
            {
                var childAssembly = assembly.CreateNewAssembly(childNode.Name);
                ProcessNode(model, childNode, childAssembly);
            }
            ProcessMeshes(model, node, assembly);
        }

        private void ProcessMeshes(
            Assimp.Scene model, 
            Node node, 
            ISceneAssembly assembly)
        {
            foreach (var meshIndex in node.MeshIndices)
            {
                var mesh = model.Meshes[meshIndex];
                int id = 0;

                var vertices =
                    mesh.Vertices
                    .Select(v =>
                    {
                        var vert = 
                        new Core.Render.Vertex(
                            id, 
                            new Core.Maths.Vector3D(v.X, v.Y, v.Z)
                        );
                        id++;

                        return vert;
                    })
                    .ToArray();

                var indices = mesh.GetUnsignedIndices();

                ISceneEntity entity = assembly.CreateNewEntity(mesh.Name);
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