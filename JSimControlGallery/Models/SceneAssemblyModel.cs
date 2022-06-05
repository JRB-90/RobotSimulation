using JSim.Core.Common;
using JSim.Core.SceneGraph;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JSimControlGallery.Models
{
    internal class SceneAssemblyModel : SceneObjectModel
    {
        public SceneAssemblyModel(ISceneAssembly sceneAssembly)
          :
            base(sceneAssembly)
        {
            SceneAssembly = sceneAssembly;
            SceneAssembly.SceneObjectModified += OnSceneAssemblyModified;

            var childModels = new List<SceneObjectModel>();

            foreach (var child in SceneAssembly.Children)
            {
                if (child is ISceneAssembly assembly)
                {
                    childModels.Add(new SceneAssemblyModel(assembly));
                }
                else if (child is ISceneEntity entity)
                {
                    childModels.Add(new SceneEntityModel(entity));
                }
                else
                {
                    childModels.Add(new SceneObjectModel(child));
                }
            }

            Children = new ObservableCollection<SceneObjectModel>(childModels);
        }

        public ISceneAssembly SceneAssembly { get; }

        public override string Icon =>
            SceneAssembly.ParentAssembly == null
                ? "fa-light fa-list-ul"
                : "fa-light fa-object-group";

        public ObservableCollection<SceneObjectModel> Children { get; }

        public override bool MoveAssembly(SceneAssemblyModel parentAssembly)
        {
            if (SceneAssembly.MoveAssembly(parentAssembly.SceneAssembly))
            {
                SetExpanded(parentAssembly);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnSceneAssemblyModified(
            object sender, 
            SceneObjectModifiedEventArgs e)
        {
            var childrenToRemove = new List<SceneObjectModel>();

            foreach (var child in Children)
            {
                if (!SceneAssembly.Children.Contains(child.SceneObject))
                {
                    childrenToRemove.Add(child);
                }
            }

            foreach (var child in childrenToRemove)
            {
                Children.Remove(child);
            }

            var sceneObjects =
                Children
                .Select(o => o.SceneObject);

            foreach (var child in SceneAssembly.Children)
            {
                if (!sceneObjects.Contains(child))
                {
                    if (child is ISceneAssembly assembly)
                    {
                        Children.Add(new SceneAssemblyModel(assembly));
                    }
                    else if (child is ISceneEntity entity)
                    {
                        Children.Add(new SceneEntityModel(entity));
                    }
                    else
                    {
                        Children.Add(new SceneObjectModel(child));
                    }

                    IsExpanded = true;
                }
            }
        }

        private void SetExpanded(SceneAssemblyModel parentAssembly)
        {
            var flatListBefore =
                Children
                .OfType<SceneAssemblyModel>()
                .Flatten(o => o.Children.OfType<SceneAssemblyModel>())
                .ToList();

            flatListBefore.Add(this);

            var flatListAfter =
                parentAssembly
                .Children
                .OfType<SceneAssemblyModel>()
                .Flatten(o => o.Children.OfType<SceneAssemblyModel>())
                .ToDictionary(o => o.SceneAssembly, o => o);

            foreach (var child in flatListBefore)
            {
                if (flatListAfter.ContainsKey(child.SceneAssembly))
                {
                    flatListAfter[child.SceneAssembly].IsExpanded = child.IsExpanded;
                }
            }
        }
    }
}
