using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.Models
{
    internal class SceneAssemblyModel : SceneObjectModelBase
    {
        public SceneAssemblyModel(ISceneAssembly assembly)
          :
            base(
                assembly,
                "Group"
            )
        {
            Assembly = assembly;
            assembly.SceneObjectModified += OnAssemblyModified;
        }

        public ISceneAssembly Assembly { get; }

        public IReadOnlyCollection<SceneObjectModelBase> Children =>
            FormChildren();

        public void AddAssembly()
        {
            Assembly.CreateNewAssembly();
        }

        public void AddEntity()
        {
            Assembly.CreateNewEntity();
        }

        private IReadOnlyCollection<SceneObjectModelBase> FormChildren()
        {
            var children = new List<SceneObjectModelBase>();

            foreach (var sceneObject in Assembly.Children)
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
