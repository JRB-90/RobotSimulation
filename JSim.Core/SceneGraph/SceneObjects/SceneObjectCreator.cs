using JSim.Core.Common;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a scene object creator.
    /// </summary>
    public class SceneObjectCreator : ISceneObjectCreator
    {
        readonly ILogger logger;
        readonly ISceneAssemblyFactory assemblyFactory;
        readonly ISceneEntityFactory entityFactory;
        readonly INameRepository nameRepository;

        public SceneObjectCreator(
            ILogger logger,
            ISceneAssemblyFactory assemblyFactory,
            ISceneEntityFactory entityFactory,
            INameRepositoryFactory nameRepositoryFactory)
        {
            this.logger = logger;
            this.assemblyFactory = assemblyFactory;
            this.entityFactory = entityFactory;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
            logger.Log("SceneObjectCreator initialised", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("SceneObjectCreator disposed", LogLevel.Debug);
        }

        /// <summary>
        /// Creates a new instance of a scene assembly.
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// </summary>
        public ISceneAssembly CreateSceneAssembly(ISceneAssembly? parentAssembly)
        {
            return 
                assemblyFactory.CreateSceneAssembly(
                    nameRepository,
                    this,
                    parentAssembly
                );
        }

        /// <summary>
        /// Creates a new instance of a scene entity.
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// </summary>
        public ISceneEntity CreateSceneEntity(ISceneAssembly parentAssembly)
        {
            return 
                entityFactory.CreateSceneEntity(
                    nameRepository,
                    parentAssembly
                );
        }
    }
}
