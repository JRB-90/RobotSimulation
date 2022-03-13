namespace JSim.Core.SceneGraph
{
    public interface INameRepositoryFactory
    {
        INameRepository CreateNameRepository();

        void Destroy(INameRepository nameRepository);
    }
}
