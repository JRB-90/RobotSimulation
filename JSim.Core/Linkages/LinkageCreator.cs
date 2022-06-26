using JSim.Core.Common;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Standard implementation of a linkage creator.
    /// </summary>
    public class LinkageCreator : ILinkageCreator
    {
        readonly ILogger logger;
        readonly INameRepository nameRepository;
        readonly IMessageCollator messageCollator;
        readonly ILinkageFactory linkageFactory;

        public LinkageCreator(
            ILogger logger,
            INameRepositoryFactory nameRepositoryFactory,
            IMessageCollator messageCollator,
            ILinkageFactory linkageFactory)
        {
            this.logger = logger;
            nameRepository = nameRepositoryFactory.CreateNameRepository();
            this.messageCollator = messageCollator;
            this.linkageFactory = linkageFactory;
            logger.Log("LinkageCreator initialised", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("LinkageCreator disposed", LogLevel.Debug);
        }

        public ILinkage CreateRootLinkage()
        {
            return
                linkageFactory.CreateRootLinkage(
                    nameRepository,
                    messageCollator,
                    this
                );
        }

        public ILinkage CreateLinkage(ILinkage parentLinkage)
        {
            return
                linkageFactory.CreateLinkage(
                    nameRepository,
                    messageCollator,
                    this,
                    parentLinkage
                );
        }
    }
}
