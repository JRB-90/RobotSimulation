using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.Models
{
    internal abstract class SceneObjectModelBase : ReactiveObject
    {
        readonly ISceneObject sceneObject;

        public SceneObjectModelBase(
            ISceneObject sceneObject,
            string iconName)
        {
            this.sceneObject = sceneObject;
            IconName = iconName;
            sceneObject.SceneObjectModified += OnSceneObjectModified;
        }

        public string IconName { get; }

        public string Name =>
            sceneObject.Name;

        public void Remove()
        {
            sceneObject.ParentAssembly?.DetachObject(sceneObject);
        }

        public void Move(ISceneAssembly assembly)
        {
            sceneObject.MoveAssembly(assembly);
        }

        private void OnSceneObjectModified(object sender, SceneObjectModifiedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Name));
        }
    }
}
