using JSim.Core.Common;

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
            nameRepository.AddName(name);
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
                RaiseSceneObjectChangedEvent();
                // TODO - Check for null then recalculate world position
            }
        }

        public event SceneObjectModifiedEventHandler? SceneObjectModified;

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

        protected void RaiseSceneObjectChangedEvent()
        {
            collator.Publish(new SceneObjectModified(this));
            SceneObjectModified?.Invoke(this, new SceneObjectModifiedEventArgs(this));
        }

        protected IMessageCollator collator;

        private string name;
        private ISceneAssembly? parentAssembly;
    }
}
