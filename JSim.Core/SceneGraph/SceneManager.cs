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
            ISceneFactory sceneFactory)
        {
            this.logger = logger;
            this.sceneFactory = sceneFactory;
            CurrentScene = sceneFactory.GetScene();
            logger.Log("SceneManager initialised", LogLevel.Debug);
        }

        public IScene CurrentScene { get; private set; }

        public void Dispose()
        {
            // TODO - Dispose scene graph

            logger.Log("SceneManager disposed", LogLevel.Debug);
        }
    }
}
