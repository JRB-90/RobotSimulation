using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSimControlGallery.Models
{
    internal class SceneObjectModel : ReactiveObject
    {
        public SceneObjectModel(ISceneObject sceneObject)
        {
            SceneObject = sceneObject;
            Name = sceneObject.Name;
        }

        public ISceneObject SceneObject { get; }

        public string Name { get; }

        public virtual string Icon =>
            "fa-square-o";

        public bool IsExpanded
        {
            get => isExpanded;
            set => this.RaiseAndSetIfChanged(ref isExpanded, value);
        }

        public virtual bool MoveAssembly(SceneAssemblyModel parentAssembly)
        {
            if (SceneObject.MoveAssembly(parentAssembly.SceneAssembly))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove()
        {
            SceneObject.ParentAssembly?.DetachObject(SceneObject);
        }

        public bool CanRemove(object parameter)
        {
            return SceneObject.ParentAssembly != null;
        }

        private bool isExpanded;
    }
}
