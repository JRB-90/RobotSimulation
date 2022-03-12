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
            index = 0;
            names = new List<string>();
        }

        public bool IsUniqueName(string name)
        {
            return !names.Contains(name);
        }

        public string GenerateUniqueName(bool addAfterCreation)
        {
            string generatedName;
            do
            {
                generatedName = $"{UniqueNameBase}{index}";
                index++;
            } while (!IsUniqueName(generatedName));

            if (addAfterCreation)
            {
                names.Add(generatedName);
            }

            return generatedName;
        }

        public bool AddName(string name)
        {
            if (names.Contains(name))
            {
                return false;
            }
            else
            {
                names.Add(name);

                return true;
            }
        }

        public bool RemoveName(string name)
        {
            if (names.Contains(name))
            {
                names.Remove(name);

                return true;
            }
            else
            {
                return false;
            }
        }

        private int index;
        private List<string> names;
    }
}
