using JSim.Core.Maths;

namespace JSim.Core.Common
{
    /// <summary>
    /// Represents an object that is positionable inside the 3D world.
    /// </summary>
    public interface IPositionable
    {
        /// <summary>
        /// The position of the object in world coordinates.
        /// </summary>
        Transform3D WorldFrame { get; set; }

        /// <summary>
        /// The position of the object in local coordinates, i.e. relative
        /// to it's parent.
        /// </summary>
        Transform3D LocalFrame { get; set; }

        /// <summary>
        /// Event fired when the positionable object has moved.
        /// </summary>
        event PositionModifiedEventHandler? PositionModified;
    }
}
