namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation class for a JSim scene manager.
    /// </summary>
    public class SceneManager : ISceneManager
    {
        readonly ILogger logger;
        readonly ISceneFactory sceneFactory;

        public SceneManager(
            ILogger logger,
            ISceneFactory sceneFactory,
            ISceneObjectCreator sceneObjectCreator)
        {
            this.logger = logger;
            this.sceneFactory = sceneFactory;
            SceneObjectCreator = sceneObjectCreator;
            CurrentScene = sceneFactory.GetScene();
            logger.Log("SceneManager initialised", LogLevel.Debug);
        }

        public ISceneObjectCreator SceneObjectCreator { get; }

        public IScene CurrentScene { get; private set; }

        public void Dispose()
        {
            // TODO - Dispose scene graph

            logger.Log("SceneManager disposed", LogLevel.Debug);
        }
    }
}
