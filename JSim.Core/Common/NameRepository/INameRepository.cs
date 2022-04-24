namespace JSim.Core.Common
{
    /// <summary>
    /// Interface to define behaviour of scene graph name repositories,
    /// which are used to store all the names of objects in a scene so
    /// that uniqueness can be maintained and also new names generated.
    /// </summary>
    public interface INameRepository
    {
        bool IsUniqueName(string name);

        string GenerateUniqueName(
            string nameRoot = "Object",
            bool addAfterCreation = true
        );

        bool AddName(string name);

        bool RemoveName(string name);
    }
}
