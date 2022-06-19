using JSim.Core.Common;

namespace JSim.Core.Render
{
    public interface IGeometryCreatorFactory
    {
        IGeometryCreator CreateGeometryCreator();

        IGeometryCreator CreateGeometryCreator(IMessageCollator messageCollator);

        void Destroy(IGeometryCreator creator);
    }
}
