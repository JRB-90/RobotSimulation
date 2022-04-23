using JSim.Core.Common;
using JSim.Core.Maths;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Abstract base class for all standard scene object implementations.
    /// </summary>
    public abstract class SceneObjectBase : ISceneObject
    {
        readonly INameRepository nameRepository;

        public SceneObjectBase(
            INameRepository nameRepository,
            IMessageCollator collator)
        {
            this.nameRepository = nameRepository;
            this.collator = collator;
            ID = Guid.NewGuid();
            name = nameRepository.GenerateUniqueName(true);
            parentAssembly = null;
            worldFrame = new Transform3D();
            localFrame = new Transform3D();
            isSelected = false;
        }

        public SceneObjectBase(
            INameRepository nameRepository,
            IMessageCollator collator,
            ISceneAssembly? parentAssembly)
        {
            this.nameRepository = nameRepository;
            this.collator = collator;
            ID = Guid.NewGuid();
            name = nameRepository.GenerateUniqueName(true);
            this.parentAssembly = parentAssembly;
            localFrame = Transform3D.Identity;

            if (parentAssembly != null)
            {
                worldFrame = parentAssembly.WorldFrame;
                parentAssembly.SceneObjectMoved += OnParentAssemblyMoved;
            }
            else
            {
                worldFrame = Transform3D.Identity;
            }

            isSelected = false;
        }

        public SceneObjectBase(
            INameRepository nameRepository,
            IMessageCollator collator,
            Guid id,
            string name,
            ISceneAssembly? parentAssembly)
        {
            if (!nameRepository.IsUniqueName(name))
            {
                throw new ArgumentException("Name is not unique");
            }

            this.nameRepository = nameRepository;
            this.collator = collator;
            ID = id;
            this.name = name;
            this.parentAssembly = parentAssembly;
            localFrame = Transform3D.Identity;
            nameRepository.AddName(name);

            if (parentAssembly != null)
            {
                worldFrame = parentAssembly.WorldFrame;
                parentAssembly.SceneObjectMoved += OnParentAssemblyMoved;
            }
            else
            {
                worldFrame = Transform3D.Identity;
            }

            isSelected = false;
        }

        /// <summary>
        /// Unique ID for the object.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Unique name for the object.
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
                    RaiseSceneObjectChangedEvent();
                }
            }
        }

        /// <summary>
        /// Parent scene assembly that this object is attached to.
        /// Null if it is the root node.
        /// </summary>
        public ISceneAssembly? ParentAssembly
        {
            get => parentAssembly;
            private set
            {
                parentAssembly = value;

                if (parentAssembly != null)
                {
                    LocalFrame = new Transform3D(localFrame);
                    parentAssembly.SceneObjectMoved += OnParentAssemblyMoved;
                }
                else
                {
                    WorldFrame = Transform3D.Identity;
                }

                RaiseSceneObjectChangedEvent();
            }
        }

        /// <summary>
        /// The position of the object in world coordinates.
        /// </summary>
        public Transform3D WorldFrame
        {
            get => worldFrame;
            set
            {
                worldFrame = value;

                if (parentAssembly != null)
                {
                    localFrame = 
                        Transform3D.RelativeTransform(
                            parentAssembly.WorldFrame, 
                            worldFrame
                        );
                }
                else
                {
                    localFrame = worldFrame;
                }

                RaiseSceneObjectMovedEvent();
                RaiseSceneObjectChangedEvent();
            }
        }

        /// <summary>
        /// The position of the object in local coordinates, i.e. relative
        /// to it's parent assembly.
        /// </summary>
        public Transform3D LocalFrame
        {
            get => localFrame;
            set
            {
                localFrame = value;

                if (parentAssembly != null)
                {
                    worldFrame = parentAssembly.WorldFrame * localFrame;
                }
                else
                {
                    worldFrame = localFrame;
                }

                RaiseSceneObjectMovedEvent();
                RaiseSceneObjectChangedEvent();
            }
        }

        /// <summary>
        /// Tracks the selection state of the scene object.
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                SelectionStateChanged?.Invoke(this, new SelectionStateChangedEventArgs(isSelected));
            }
        }

        /// <summary>
        /// Event fired when this scene object has been modified.
        /// </summary>
        public event SceneObjectModifiedEventHandler? SceneObjectModified;

        /// <summary>
        /// Event fired when this scene object has moved.
        /// </summary>
        public event SceneObjectMovedEventHandler? SceneObjectMoved;

        /// <summary>
        /// Event fired when the selection state of this object has changed.
        /// </summary>
        public event SelectionStateChangedEventHandler? SelectionStateChanged;

        /// <summary>
        /// Moves the scene object to a new assembly.
        /// </summary>
        /// <param name="newAssembly">New assembly to attach this acene object to.</param>
        /// <returns>True if move was successful.</returns>
        public bool MoveAssembly(ISceneAssembly newParent)
        {
            if (ParentAssembly == null)
            {
                return false;
            }

            if (!ParentAssembly.DetachObject(this))
            {
                return false;
            }

            if (newParent.AttachObject(this))
            {
                ParentAssembly = newParent;
                RaiseSceneObjectChangedEvent();

                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Name}";
        }

        /// <summary>
        /// Raises a SceneObjectModified event for this object.
        /// </summary>
        protected void RaiseSceneObjectChangedEvent()
        {
            collator.Publish(new SceneObjectModified(this));
            SceneObjectModified?.Invoke(this, new SceneObjectModifiedEventArgs(this));
        }

        /// <summary>
        /// Raises a SceneObjectMoved event for this object.
        /// </summary>
        protected void RaiseSceneObjectMovedEvent()
        {
            collator.Publish(new SceneObjectMoved(this));
            SceneObjectMoved?.Invoke(this, new SceneObjectMovedEventArgs(this));
        }

        private void OnParentAssemblyMoved(object sender, SceneObjectMovedEventArgs e)
        {
            LocalFrame = LocalFrame;
        }

        protected IMessageCollator collator;

        private string name;
        private ISceneAssembly? parentAssembly;
        private Transform3D worldFrame;
        private Transform3D localFrame;
        private bool isSelected;
    }
}
