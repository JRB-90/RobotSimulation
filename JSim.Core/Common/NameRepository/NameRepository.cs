namespace JSim.Core.Common
{
    /// <summary>
    /// Standard implementation of a name repository.
    /// </summary>
    public class NameRepository : INameRepository
    {
        public NameRepository()
        {
            names = new List<string>();
        }

        public bool IsUniqueName(string name)
        {
            return !names.Contains(name);
        }

        public string GenerateUniqueName(
            string nameRoot = "Object",
            bool addAfterCreation = true)
        {
            int index = 1;
            string generatedName;

            do
            {
                generatedName = $"{nameRoot}{index}";
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

        private List<string> names;
    }
}
