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
            ISceneObjectCreator creator)
          :
            base(nameRepository)
        {
            this.creator = creator;
            children = new List<ISceneObject>();
        }

        public SceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
            ISceneAssembly? parentAssembly)
          :
            base(
                nameRepository,
                parentAssembly)
        {
            this.creator = creator;
            children = new List<ISceneObject>();
        }

        public SceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
            Guid id,
            string name,
            ISceneAssembly parentAssembly)
          :
            base(
                nameRepository,
                id,
                name,
                parentAssembly)
        {
            this.creator = creator;
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

            return assembly;
        }

        public ISceneEntity CreateNewEntity()
        {
            ISceneEntity entity = creator.CreateSceneEntity(this);
            children.Add(entity);

            return entity;
        }

        public bool RemoveObject(ISceneObject sceneObject)
        {
            if (children.Contains(sceneObject))
            {
                children.Remove(sceneObject);

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
