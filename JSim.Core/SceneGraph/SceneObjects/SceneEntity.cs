using JSim.Core.Common;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene entity.
    /// </summary>
    public class SceneEntity : SceneObjectBase, ISceneEntity
    {
        public SceneEntity(
            INameRepository nameRepository,
            IMessageCollator collator,
            ISceneAssembly parentAssembly,
            string nameRoot = "Entity")
          :
            base(
                nameRepository,
                collator,
                parentAssembly,
                nameRoot)
        {
        }

        public SceneEntity(
            INameRepository nameRepository,
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
        }
    }
}
