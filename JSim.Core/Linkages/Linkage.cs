using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Represents the basic implementation of a linkage.
    /// </summary>
    public class Linkage
      :
        PositionableHierarcyTreeObjectBase<ILinkage>,
        ILinkage
    {
        readonly ILinkageCreator linkageCreator;

        public Linkage(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            ILinkageCreator linkageCreator,
            IGeometryContainer geometryContainer)
          :
            base(
                nameRepository,
                messageCollator,
                "Link"
            )
        {
            this.linkageCreator = linkageCreator;
            GeometryContainer = geometryContainer;
        }

        public Linkage(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            ILinkageCreator linkageCreator,
            ILinkage parentLinkage,
            IGeometryContainer geometryContainer)
          :
            base(
                nameRepository,
                messageCollator,
                parentLinkage,
                "Link"
            )
        {
            this.linkageCreator = linkageCreator;
            GeometryContainer = geometryContainer;
        }

        /// <summary>
        /// Container for the geometry attached to this link.
        /// </summary>
        public IGeometryContainer GeometryContainer { get; }

        /// <summary>
        /// Creates a new linkage attached to this one.
        /// </summary>
        /// <returns>Newly created linkage object.</returns>
        public ILinkage CreateNewLinkage()
        {
            var linkage = linkageCreator.CreateLinkage(this);
            if (childContainer.AttachChild(linkage))
            {
                RaiseObjectModified();

                return linkage;
            }
            else
            {
                throw new InvalidOperationException("Failed to create a child linkage");
            }
        }
    }
}
