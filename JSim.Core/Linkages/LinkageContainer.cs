using JSim.Core.Common;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Standard implementation of a linkage container.
    /// </summary>
    public class LinkageContainer : ILinkageContainer
    {
        const string DEFAULT_BASE_LINK_NAME = "Base";

        readonly ILogger logger;
        readonly IMessageCollator messageCollator;
        readonly ILinkageCreator linkageCreator;

        public LinkageContainer(
            ILogger logger,
            IMessageCollator messageCollator,
            ILinkageCreatorFactory linkageCreatorFactory)
        {
            this.logger = logger;
            this.messageCollator = messageCollator;
            linkageCreator = linkageCreatorFactory.CreateLinkageCreator(messageCollator);
            BaseLinkage = linkageCreator.CreateRootLinkage();
            BaseLinkage.Name = DEFAULT_BASE_LINK_NAME;
        }

        public ILinkage BaseLinkage { get; }
    }
}
