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

            childContainer.ChildContainerModified += OnChildrenChanged;
        }

        public Linkage(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            ILinkageCreator linkageCreator,
            ILinkage parentLinkage,
            IGeometryContainer geometryContainer)
        {
            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;
            this.linkageCreator = linkageCreator;

            name = nameRepository.GenerateUniqueName("Link");
            id = Guid.NewGuid();
            parent = parentLinkage;
            childContainer = new ChildContainer<ILinkage>();
            GeometryContainer = geometryContainer;
            worldFrame = Transform3D.Identity;
            localFrame = Transform3D.Identity;

            childContainer.ChildContainerModified += OnChildrenChanged;
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

        public ILinkage? Parent => parent;

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

        /// <summary>
        /// Event fired when the position of the linakge has been modified.
        /// </summary>
        public event PositionModifiedEventHandler? PositionModified;

        /// <summary>
        /// Event fired when the properties of the linkage has been modified.
        /// </summary>
        public event TreeObjectModifiedEventHandler? ObjectModified;

        /// <summary>
        /// Attaches the linkage to another linakage node.
        /// </summary>
        /// <param name="newParent">New parent linkage to attach this node to.</param>
        /// <returns>True if move was successful.</returns>
        public bool AttachTo(ILinkage? newParent)
        {
            if (newParent == null)
            {
                if (parent == null)
                {
                    return true;
                }
                else
                {
                    parent.DetachChild(this);
                    parent = null;

                    return true;
                }
            }
            else
            {
                if (newParent.Children.Contains(this))
                {
                    return false;
                }
                else
                {
                    newParent.DetachChild(this);
                    //newParent.AttachChild(this);
                    parent = newParent;

                    return true;
                }
            }
        }

        /// <summary>
        /// Removes the linkage from it's parent.
        /// </summary>
        /// <returns>True if successful.</returns>
        public bool Detach()
        {
            if (parent == null)
            {
                return false;
            }
            else
            {
                return AttachTo(null);
            }
        }

        /// <summary>
        /// Attaches a given linkage node to this object.
        /// </summary>
        /// <param name="child">Child node to attach.</param>
        /// <returns>True if successful. False if child is already attached.</returns>
        public bool AttachChild(ILinkage child)
        {
            if (!Children.Contains(this))
            {
                var res = 
                    child.AttachTo(this) && 
                    childContainer.AttachChild(child);

                return res;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Detaches a given linkage from this objects children.
        /// </summary>
        /// <param name="child">Child to be detached.</param>
        /// <returns>True if successful, False if child cannot be found.</returns>
        public bool DetachChild(ILinkage child)
        {
            if (childContainer.DetachChild(child) &&
                child.AttachTo(null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        private void RaiseObjectModified()
        {
            ObjectModified?.Invoke(this, new TreeObjectModifiedEventArgs(this));
        }

        private void OnChildrenChanged(object sender, ChildContainerModifiedEventArgs e)
        {
            RaiseObjectModified();
        }

        private string name;
        private Guid id;
        private ILinkage? parent;
        private IChildContainer<ILinkage> childContainer;
        private Transform3D worldFrame;
        private Transform3D localFrame;
    }
}
