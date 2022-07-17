namespace JSim.Core.Common
{
    public interface IPositionableHierarcyTreeObject<T>
      :
        IHierarchicalTreeObject<T, T>, 
        IPositionable
        where T : ITreeObject
    {
    }
}
