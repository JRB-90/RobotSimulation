using JSim.Core.Common;
using System.Collections;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene assembly.
    /// </summary>
    public class SceneAssembly : SceneObjectBase, ISceneAssembly
    {
        readonly ISceneObjectCreator creator;

        public SceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
            IMessageCollator collator)
          :
            base(
                nameRepository,
                collator)
        {
            this.creator = creator;
            this.collator = collator;
            children = new List<ISceneObject>();
        }

        public SceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
            IMessageCollator collator,
            ISceneAssembly? parentAssembly)
          :
            base(
                nameRepository,
                collator,
                parentAssembly)
        {
            this.creator = creator;
            this.collator = collator;
            children = new List<ISceneObject>();
        }

        public SceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
            IMessageCollator collator,
            Guid id,
            string name,
            ISceneAssembly parentAssembly)
          :
            base(
                nameRepository,
                collator,
                id,
                name,
                parentAssembly)
        {
            this.creator = creator;
            this.collator = collator;
            children = new List<ISceneObject>();
        }

        public IReadOnlyCollection<ISceneObject> Children
        {
            get => children;
        }

        public ISceneAssembly CreateNewAssembly()
        {
            ISceneAssembly assembly = creator.CreateSceneAssembly(this);
            children.Add(assembly);
            RaiseSceneObjectChangedEvent();
            collator.Publish(new SceneStructureChanged(this));

            return assembly;
        }

        public ISceneAssembly CreateNewAssembly(string name)
        {
            ISceneAssembly assembly = creator.CreateSceneAssembly(this);
            assembly.Name = name;
            children.Add(assembly);
            RaiseSceneObjectChangedEvent();
            collator.Publish(new SceneStructureChanged(this));

            return assembly;
        }

        public ISceneEntity CreateNewEntity()
        {
            ISceneEntity entity = creator.CreateSceneEntity(this);
            children.Add(entity);
            RaiseSceneObjectChangedEvent();
            collator.Publish(new SceneStructureChanged(this));

            return entity;
        }

        public ISceneEntity CreateNewEntity(string name)
        {
            ISceneEntity entity = creator.CreateSceneEntity(this);
            entity.Name = name;
            children.Add(entity);
            RaiseSceneObjectChangedEvent();
            collator.Publish(new SceneStructureChanged(this));

            return entity;
        }

        public bool AttachObject(ISceneObject sceneObject)
        {
            if (Children.Contains(sceneObject))
            {
                return false;
            }
            else
            {
                children.Add(sceneObject);
                RaiseSceneObjectChangedEvent();
                collator.Publish(new SceneStructureChanged(this));

                return true;
            }
        }

        public bool DetachObject(ISceneObject sceneObject)
        {
            if (children.Contains(sceneObject))
            {
                children.Remove(sceneObject);
                RaiseSceneObjectChangedEvent();
                collator.Publish(new SceneStructureChanged(this));

                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<ISceneObject> GetEnumerator()
        {
            foreach (ISceneObject sceneObject in IterateAssembly(this))
            {
                yield return sceneObject;
            }
        }

        private IEnumerable<ISceneObject> IterateAssembly(ISceneAssembly sceneAssembly)
        {
            foreach (ISceneAssembly assembly in sceneAssembly.Children.OfType<ISceneAssembly>())
            {
                foreach (ISceneObject sceneObject in IterateAssembly(assembly))
                {
                    yield return sceneObject;
                }

                yield return assembly;
            }

            foreach (ISceneEntity entity in sceneAssembly.Children.OfType<ISceneEntity>())
            {
                yield return entity;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private List<ISceneObject> children;
    }
}
