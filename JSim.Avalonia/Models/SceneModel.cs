using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.Models
{
    internal class SceneModel : ReactiveObject
    {
        public SceneModel(IScene scene)
        {
            Scene = scene;
            scene.Root.SceneObjectModified += OnRootModified;
        }

        public IScene Scene { get; }

        public string IconName =>
            "FamilyTree";

        public string SceneName
        {
            get => Scene.Name;
            set => Scene.Name = value;
        }

        public IReadOnlyCollection<SceneObjectModelBase> Children =>
            FormChildren();

        public void AddAssembly()
        {
            Scene.Root.CreateNewAssembly();
        }

        public void AddEntity()
        {
            Scene.Root.CreateNewEntity();
        }

        private IReadOnlyCollection<SceneObjectModelBase> FormChildren()
        {
            var children = new List<SceneObjectModelBase>();

            foreach (var sceneObject in Scene.Root.Children)
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

        private void OnRootModified(object sender, SceneObjectModifiedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Children));
        }
    }
}
