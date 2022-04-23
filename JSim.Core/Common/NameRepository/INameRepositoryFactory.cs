namespace JSim.Core.Common
{
    public interface INameRepositoryFactory
    {
        INameRepository CreateNameRepository();

        void Destroy(INameRepository nameRepository);
    }
}
