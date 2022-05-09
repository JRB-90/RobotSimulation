using JSim.Core.Importer;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation class for a JSim scene manager.
    /// </summary>
    public class SceneManager : ISceneManager
    {
        readonly ILogger logger;
        readonly ISceneFactory sceneFactory;
        readonly ISceneIOHandler sceneIOHandler;

        public SceneManager(
            ILogger logger,
            ISceneFactory sceneFactory,
            ISceneIOHandler sceneIOHandler,
            IModelImporter modelImporter)
        {
            this.logger = logger;
            this.sceneFactory = sceneFactory;
            this.sceneIOHandler = sceneIOHandler;
            currentScene = sceneFactory.GetScene();
            ModelImporter = modelImporter;
            logger.Log("SceneManager initialised", LogLevel.Debug);
        }

        public IScene CurrentScene
        {
            get => currentScene;
            set
            {
                currentScene = value;
                CurrentSceneChanged?.Invoke(this, new CurrentSceneChangedEventArgs(value));
            }
        }

        public IModelImporter ModelImporter { get; }

        public event CurrentSceneChangedEventHandler? CurrentSceneChanged;

        public void Dispose()
        {
            // TODO - Dispose scene graph

            logger.Log("SceneManager disposed", LogLevel.Debug);
        }

        public void NewScene()
        {
            CurrentScene.Dispose();
            CurrentScene = sceneFactory.GetScene();
            logger.Log("SceneManager new scene", LogLevel.Debug);
        }

        public void SaveScene(string path)
        {
            sceneIOHandler.SaveSceneToFile(CurrentScene, path);
            logger.Log("SceneManager saved scene", LogLevel.Debug);
        }

        public void LoadScene(string path)
        {
            IScene newScene = sceneIOHandler.LoadSceneFromFile(path);
            CurrentScene.Dispose();
            CurrentScene = newScene;
            logger.Log("SceneManager loaded scene", LogLevel.Debug);
        }

        private IScene currentScene;
    }
}
