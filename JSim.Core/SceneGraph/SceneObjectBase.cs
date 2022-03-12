namespace JSim.Core.SceneGraph
{
    public class SceneObjectBase : ISceneObject
    {
        readonly INameRepository nameRepository;

        public SceneObjectBase(INameRepository nameRepository)
        {
            this.nameRepository = nameRepository;
            ID = Guid.NewGuid();
            name = nameRepository.GenerateUniqueName();
        }

        public Guid ID { get; }

        public string Name
        {
            get => name;
            set => name = value;
        }

        private string name;
    }
}
