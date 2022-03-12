namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene entity.
    /// </summary>
    public class SceneEntity : SceneObjectBase, ISceneEntity
    {
        public SceneEntity(INameRepository nameRepository)
          :
            base(nameRepository)
        {
            ParentAssembly = null;
        }

        public ISceneAssembly? ParentAssembly
        {
            get => parentAssembly;
            set => parentAssembly = value;
        }

        private ISceneAssembly? parentAssembly;
    }
}
