using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.Models
{
    internal class SceneAssemblyModel : SceneObjectModelBase
    {
        readonly ISceneAssembly assembly;

        public SceneAssemblyModel(ISceneAssembly assembly)
          :
            base(
                assembly,
                "Group"
            )
        {
            this.assembly = assembly;
            assembly.SceneObjectModified += OnAssemblyModified;
        }

        public IReadOnlyCollection<SceneObjectModelBase> Children =>
            FormChildren();

        public void AddAssembly()
        {
            assembly.CreateNewAssembly();
        }

        public void AddEntity()
        {
            assembly.CreateNewEntity();
        }

        private IReadOnlyCollection<SceneObjectModelBase> FormChildren()
        {
            var children = new List<SceneObjectModelBase>();

            foreach (var sceneObject in assembly.Children)
            {
                if (sceneObject is ISceneAssembly assembly)
                {
                    children.Add(new SceneAssemblyModel(assembly));
                }
                else if (sceneObject is ISceneEntity entity)
                {
                    children.Add(new SceneEntityModel(entity));
                }
            }

            return children;
        }

        private void OnAssemblyModified(object sender, SceneObjectModifiedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Children));
        }
    }
}
