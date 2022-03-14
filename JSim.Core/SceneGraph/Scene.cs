using System.Collections;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a scene.
    /// </summary>
    public class Scene : IScene
    {
        readonly ISceneObjectCreator creator;

        public Scene(ISceneObjectCreatorFactory creatorFactory)
        {
            creator = creatorFactory.CreateSceneObjectCreator();
            Name = "Scene";
            Root = creator.CreateSceneAssembly(null);
            Root.Name = "RootAssembly";
        }

        public string Name { get; set; }

        public ISceneAssembly Root { get; }

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
    }
}
