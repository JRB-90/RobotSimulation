namespace JSim.Core.Render
{
    public interface IGeometryCreatorFactory
    {
        IGeometryCreator CreateGeometryCreator();

        void Destroy(IGeometryCreator creator);
    }
}
