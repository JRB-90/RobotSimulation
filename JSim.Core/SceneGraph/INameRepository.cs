namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define behaviour of scene graph name repositories,
    /// which are used to store all the names of objects in a scene so
    /// that uniqueness can be maintained and also new names generated.
    /// </summary>
    public interface INameRepository
    {
        bool IsUniqueName(string name);

        string GenerateUniqueName();
    }
}
