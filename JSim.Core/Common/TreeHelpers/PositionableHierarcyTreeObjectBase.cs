using JSim.Core.Maths;

namespace JSim.Core.Common
{
    /// <summary>
    /// Base class to provide standard implementations of a positionable hierarcy tree object.
    /// </summary>
    /// <typeparam name="T">Type to </typeparam>
    public abstract class PositionableHierarcyTreeObjectBase<T>
        : IPositionableHierarcyTreeObject<T>
        where T : PositionableHierarcyTreeObjectBase<T>
    {
        readonly INameRepository nameRepository;
        readonly IMessageCollator messageCollator;

        public PositionableHierarcyTreeObjectBase(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            string generatedNameBase = "Object")
        {
            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;

            name = nameRepository.GenerateUniqueName(generatedNameBase);
            id = Guid.NewGuid();
            parent = default(T);
            childContainer = new ChildContainer<T>();
            WorldFrame = new ObservableTransform(Transform3D.Identity);
            LocalFrame = new ObservableTransform(Transform3D.Identity);
            WorldFrame.TransformModified += OnWorldFrameModified;
            LocalFrame.TransformModified += OnLocalFrameModified;
        }

        public PositionableHierarcyTreeObjectBase(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            T parentObject,
            string generatedNameBase = "Object")
        {
            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;

            name = nameRepository.GenerateUniqueName(generatedNameBase);
            id = Guid.NewGuid();
            parent = parentObject;
            childContainer = new ChildContainer<T>();
            WorldFrame = new ObservableTransform(Transform3D.Identity);
            LocalFrame = new ObservableTransform(Transform3D.Identity);
            WorldFrame.TransformModified += OnWorldFrameModified;
            LocalFrame.TransformModified += OnLocalFrameModified;
        }

        /// <summary>
        /// Name of the Object.
        /// Note: Uses a namerepository for the tree, that enforces it's name to
        /// remain unique.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (nameRepository.IsUniqueName(value))
                {
                    nameRepository.AddName(value);
                    nameRepository.RemoveName(name);
                    name = value;
                    RaiseObjectModified();
                }
            }
        }

        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        public Guid ID => id;

        /// <summary>
        /// Designates that this object is the top nodes of the tree.
        /// </summary>
        public bool IsTreeRoot => Parent == null;

        /// <summary>
        /// Parent of this object.
        /// </summary>
        public T? Parent
        {
            get => parent;
            set
            {
                parent = value;
                RaiseObjectModified();
            }
        }

        /// <summary>
        /// Container for the child nodes of this object.
        /// </summary>
        public IReadOnlyCollection<T> Children => childContainer.Children;

        /// <summary>
        /// The position of the object in world coordinates.
        /// </summary>
        public ObservableTransform WorldFrame { get; }

        /// <summary>
        /// The position of the object in local coordinates, i.e. relative
        /// to it's parent.
        /// </summary>
        public ObservableTransform LocalFrame { get; }

        /// <summary>
        /// Event fired when the properties of the object have been modified.
        /// </summary>
        public event TreeObjectModifiedEventHandler? ObjectModified;

        /// <summary>
        /// Event fired when the positionable object has moved.
        /// </summary>
        public event PositionModifiedEventHandler? PositionModified;

        /// <summary>
        /// Attaches the tree object to a new node.
        /// </summary>
        /// <param name="newParent">New parent to attach this node to.</param>
        /// <returns>True if move was successful.</returns>
        public bool AttachTo(T? newParent)
        {
            if (newParent == null)
            {
                if (parent == null)
                {
                    return true;
                }
                else
                {
                    parent.DetachChild((T)this);
                    parent = default(T);
                    RaiseObjectModified();

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
                    newParent.DetachChild((T)this);
                    newParent.AttachChild((T)this);
                    parent = newParent;
                    RaiseObjectModified();

                    return true;
                }
            }
        }

        /// <summary>
        /// Removes the object from it's parent.
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
                return AttachTo(default(T));
            }
        }

        /// <summary>
        /// Attaches a given child node to this object.
        /// </summary>
        /// <param name="child">Child node to attach.</param>
        /// <returns>True if successful. False if child is already attached.</returns>
        public bool AttachChild(T child)
        {
            if (childContainer.AttachChild(child))
            {
                child.Parent = (T)this;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Detaches a given child from this objects children.
        /// </summary>
        /// <param name="child">Child to be detached.</param>
        /// <returns>True if successful, False if child cannot be found.</returns>
        public bool DetachChild(T child)
        {
            if (childContainer.DetachChild(child) &&
                child.AttachTo(default(T)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void RaiseObjectModified()
        {
            ObjectModified?.Invoke(this, new TreeObjectModifiedEventArgs((T)this));
        }

        protected void RaisePositionModified()
        {
            PositionModified?.Invoke(this, new PositionModifiedEventArgs((T)this));
            RaiseObjectModified();
        }

        private void OnWorldFrameModified(object sender, TransformModifiedEventArgs e)
        {
            if (!isUpdatingFrames)
            {
                isUpdatingFrames = true;

                if (parent != null)
                {
                    LocalFrame.SetTransform(
                        Transform3D.RelativeTransform(
                            parent.WorldFrame.GetTransformCopy(),
                            WorldFrame.GetTransformCopy()
                        )
                    );
                }
                else
                {
                    LocalFrame.SetTransform(WorldFrame.GetTransformCopy());
                }

                isUpdatingFrames = false;
            }

            RaisePositionModified();
        }

        private void OnLocalFrameModified(object sender, TransformModifiedEventArgs e)
        {
            if (!isUpdatingFrames)
            {
                isUpdatingFrames = true;

                if (parent != null)
                {
                    WorldFrame.SetTransform(
                        parent.WorldFrame.GetTransformCopy() *
                        LocalFrame.GetTransformCopy()
                    );
                }
                else
                {
                    WorldFrame.SetTransform(LocalFrame.GetTransformCopy());
                }

                isUpdatingFrames = false;
            }

            RaisePositionModified();
        }

        private bool isUpdatingFrames;
        private string name;
        private Guid id;
        protected T? parent;
        protected IChildContainer<T> childContainer;
    }
}
