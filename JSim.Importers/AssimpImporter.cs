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
                    PostProcessSteps.FlipWindingOrder |
                    PostProcessSteps.GenerateSmoothNormals |
                    PostProcessSteps.ForceGenerateNormals
                );

            ISceneAssembly assembly = 
                parent.CreateNewAssembly(
                    Path.GetFileNameWithoutExtension(path)
                );
            
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
                var vertices = new List<Core.Render.Vertex>();
                var indices = mesh.GetUnsignedIndices();

                for (int i = 0; i < indices.Length; i++)
                {
                    var v = mesh.Vertices[i];
                    var n = mesh.Normals[i];

                    vertices.Add(
                        new Core.Render.Vertex(
                            i,
                            new Core.Maths.Vector3D(v.X, v.Y, v.Z),
                            new Core.Maths.Vector3D(-n.X, -n.Y, -n.Z)
                        )
                    );
                }

                ISceneEntity entity = assembly.CreateNewEntity(mesh.Name);
                var geo = entity.GeometryContainer.Root.CreateChildGeometry(mesh.Name);
                geo.SetDrawingData(vertices.ToArray(), indices);

                if (model.HasMaterials)
                {
                    geo.Material = ToJSimMaterial(model.Materials[mesh.MaterialIndex]);
                }
                else
                {
                    geo.Material = 
                        Core.Render.Material.FromSingleColor(
                            new Core.Render.Color(1.0f, 0.5f, 0.5f, 0.5f)
                        );
                }
            }
        }

        private Core.Render.Material ToJSimMaterial(Material material)
        {
            return
                new Core.Render.Material(
                    ToJSimColor(material.ColorAmbient),
                    ToJSimColor(material.ColorDiffuse),
                    ToJSimColor(material.ColorSpecular),
                    material.Shininess,
                    Core.Render.ShadingType.Smooth
                );
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