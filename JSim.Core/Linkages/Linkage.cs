using JSim.Core.Common;
using JSim.Core.Maths;
using JSim.Core.Render;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Represents the basic implementation of a linkage.
    /// </summary>
    public class Linkage : ILinkage
    {
        readonly INameRepository nameRepository;
        readonly IMessageCollator messageCollator;
        readonly ILinkageCreator linkageCreator;

        public Linkage(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            ILinkageCreator linkageCreator,
            IGeometryContainer geometryContainer)
        {
            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;
            this.linkageCreator = linkageCreator;

            name = nameRepository.GenerateUniqueName("Link");
            id = Guid.NewGuid();
            parent = null;
            childContainer = new ChildContainer<ILinkage>();
            GeometryContainer = geometryContainer;
            worldFrame = Transform3D.Identity;
            localFrame = Transform3D.Identity;
        }
        
        public string Name
        {
            get => name;
            set
            {
                if (nameRepository.IsUniqueName(value))
                {
                    name = value;
                    RaiseObjectModified();
                }
            }
        }

        public Guid ID => id;

        public bool IsTreeRoot => Parent == null;

        public IHierarchicalTreeObject<ITreeObject>? Parent => parent;

        public IReadOnlyCollection<ILinkage> Children => childContainer.Children;

        public IGeometryContainer GeometryContainer { get; }

        Transform3D IPositionable.WorldFrame
        {
            get => worldFrame;
            set => worldFrame = value;
        }
        Transform3D IPositionable.LocalFrame
        {
            get => localFrame;
            set => localFrame = value;
        }

        public event PositionModifiedEventHandler? PositionModified;

        public event TreeObjectModifiedEventHandler? ObjectModified;

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

        public bool Move(IHierarchicalTreeObject<ITreeObject> newContainer)
        {
            if (IsTreeRoot)
            {
                return false;
            }

            //if (!Parent.DetachGeometry(this))
            //{
            //    return false;
            //}

            //if (newContainer.AttachGeometry(this))
            //{
            //    parent = newContainer;
            //    FireGeometryModifiedEvent();

            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return false;
        }

        private void RaiseObjectModified()
        {
            ObjectModified?.Invoke(this, new TreeObjectModifiedEventArgs(this));
        }

        private string name;
        private Guid id;
        private IHierarchicalTreeObject<ITreeObject>? parent;
        private IChildContainer<ILinkage> childContainer;
        private Transform3D worldFrame;
        private Transform3D localFrame;
    }
}
