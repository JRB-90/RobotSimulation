using JSim.Core.Common;
using System.Collections;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a scene.
    /// </summary>
    public class Scene 
      : 
        IScene,
        IMessageHandler<SceneObjectModified>
    {
        readonly ILogger logger;
        readonly ISceneObjectCreator creator;
        readonly IMessageCollator collator;

        public Scene(
            ILogger logger,
            ISelectionManager selectionManager,
            ISceneObjectCreatorFactory creatorFactory,
            IMessageCollator collator)
        {
            this.logger = logger;
            SelectionManager = selectionManager;
            creator = creatorFactory.CreateSceneObjectCreator();
            this.collator = collator;
            collator.Subscribe(this);
            name = "Scene";
            Root = creator.CreateSceneAssembly(null);
            Root.Name = "RootAssembly";
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NameChanged?.Invoke(this, new SceneNameChangedEventArgs(name));
            }
        }

        public ISceneAssembly Root { get; }

        public ISelectionManager SelectionManager { get; }

        public event SceneNameChangedEventHandler? NameChanged;

        public event SceneTreeModifiedEventHandler? SceneTreeModified;

        public void Dispose()
        {
        }

        public IEnumerator<ISceneObject> GetEnumerator()
        {
            foreach (ISceneObject sceneObject in Root)
            {
                yield return sceneObject;
            }
        }

        public bool TryFindByID(Guid id, out ISceneObject? sceneObject)
        {
            sceneObject =
                this.
                Where(o => o.ID == id)
                .FirstOrDefault();

            return sceneObject != null;
        }

        public bool TryFindByName(string name, out ISceneObject? sceneObject)
        {
            sceneObject =
                this.
                Where(o => o.Name == name)
                .FirstOrDefault();

            return sceneObject != null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Handle(SceneObjectModified message)
        {
            logger.Log($"Scene tree modified: {message.SceneObject.Name}", LogLevel.Debug);
            SceneTreeModified?.Invoke(this, new SceneTreeModifiedEventArgs());
        }

        private string name;
    }
}
