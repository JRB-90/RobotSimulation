using JSim.Core.Common;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Interface defining the behaviour of all linkage creater factories.
    /// </summary>
    public interface ILinkageCreatorFactory
    {
        ILinkageCreator CreateLinkageCreator();

        ILinkageCreator CreateLinkageCreator(IMessageCollator messageCollator);

        void Destroy(ILinkageCreator creator);
    }
}
