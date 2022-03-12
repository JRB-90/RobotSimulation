namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a name repository.
    /// </summary>
    public class NameRepository : INameRepository
    {
        const string UniqueNameBase = "SceneObject_";

        public NameRepository()
        {
            index = 1;
            names = new List<string>();
        }

        public string GenerateUniqueName()
        {
            string generatedName;
            do
            {
                generatedName = $"{UniqueNameBase}{index}";
                index++;
            } while (!IsUniqueName(generatedName));

            names.Add(generatedName);

            return generatedName;
        }

        public bool IsUniqueName(string name)
        {
            return !names.Contains(name);
        }

        private int index;
        private List<string> names;
    }
}
