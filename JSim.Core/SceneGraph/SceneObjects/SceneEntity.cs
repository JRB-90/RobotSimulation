namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene entity.
    /// </summary>
    public class SceneEntity : SceneObjectBase, ISceneEntity
    {
        public SceneEntity(
            INameRepository nameRepository,
            ISceneAssembly parentAssembly)
          :
            base(
                nameRepository,
                parentAssembly)
        {
        }

        public SceneEntity(
            INameRepository nameRepository,
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
        }
    }
}
