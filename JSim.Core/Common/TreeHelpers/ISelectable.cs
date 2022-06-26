namespace JSim.Core.Common
{
    /// <summary>
    /// Represents an object that is selectable.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Tracks the selection state of the object.
        /// </summary>
        bool IsSelected { get; set; }
    }
}
