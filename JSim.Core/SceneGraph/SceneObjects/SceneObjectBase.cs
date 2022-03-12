namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Abstract base class for all standard scene object implementations.
    /// </summary>
    public abstract class SceneObjectBase : ISceneObject
    {
        readonly INameRepository nameRepository;

        public SceneObjectBase(INameRepository nameRepository)
        {
            this.nameRepository = nameRepository;
            ID = Guid.NewGuid();
            name = nameRepository.GenerateUniqueName(true);
            parentAssembly = null;
        }

        public SceneObjectBase(
            INameRepository nameRepository,
            ISceneAssembly? parentAssembly)
        {
            this.nameRepository = nameRepository;
            ID = Guid.NewGuid();
            name = nameRepository.GenerateUniqueName(true);
            this.parentAssembly = parentAssembly;
        }

        public SceneObjectBase(
            INameRepository nameRepository,
            Guid id,
            string name,
            ISceneAssembly? parentAssembly)
        {
            if (!nameRepository.IsUniqueName(name))
            {
                throw new ArgumentException("Name is not unique");
            }

            this.nameRepository = nameRepository;
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
                    // TODO - Raise event
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
            set
            {
                parentAssembly = value;
                // TODO - Check for null then recalculate world position
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private string name;
        private ISceneAssembly? parentAssembly;
    }
}
