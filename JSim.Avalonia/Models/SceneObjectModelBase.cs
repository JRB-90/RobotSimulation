using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.Models
{
    internal abstract class SceneObjectModelBase : ReactiveObject
    {
        public SceneObjectModelBase(
            ISceneObject sceneObject,
            string iconName)
        {
            SceneObject = sceneObject;
            IconName = iconName;
            sceneObject.SceneObjectModified += OnSceneObjectModified;
        }

        public ISceneObject SceneObject { get; }

        public string IconName { get; }

        public string Name =>
            SceneObject.Name;

        public void Remove()
        {
            SceneObject.ParentAssembly?.DetachObject(SceneObject);
        }

        public void Move(ISceneAssembly assembly)
        {
            SceneObject.MoveAssembly(assembly);
        }

        private void OnSceneObjectModified(object sender, SceneObjectModifiedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Name));
        }
    }
}
