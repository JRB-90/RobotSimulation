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

        private List<ISceneObject> children;
    }
}
