namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene assembly.
    /// </summary>
    public class SceneAssembly : SceneObjectBase, ISceneAssembly
    {
        public SceneAssembly(INameRepository nameRepository)
          :
            base(nameRepository)
        {
            children = new List<ISceneObject>();
        }

        public IReadOnlyCollection<ISceneObject> Children
        {
            get => children;
        }

        public bool AddChild(ISceneObject child)
        {
            if (children.Contains(child))
            {
                return false;
            }
            else
            {
                children.Add(child);
                // TODO - Fire event

                return true;
            }
        }

        public bool RemoveChild(ISceneObject child)
        {
            if (!children.Contains(child))
            {
                return false;
            }
            else
            {
                children.Remove(child);
                // TODO - Fire event

                return true;
            }
        }

        private List<ISceneObject> children;
    }
}
