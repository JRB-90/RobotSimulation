using Castle.MicroKernel;

namespace JSim.Core.SceneGraph
{
    public interface ISceneObjectCreatorFactory
    {
        ISceneObjectCreator CreateSceneObjectCreator();

        void Destroy(ISceneObjectCreator creator);
    }
}
