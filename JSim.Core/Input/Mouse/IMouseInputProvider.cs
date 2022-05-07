namespace JSim.Core.Input
{
    /// <summary>
    /// Provides an abstraction for all mouse event generators.
    /// </summary>
    public interface IMouseInputProvider
    {
        /// <summary>
        /// Event raised when the mouse has moved.
        /// </summary>
        event MouseMovedEventHandler? MouseMoved;

        /// <summary>
        /// Event raised when a mouse button has been pressed.
        /// </summary>
        event MouseButtonDownEventHandler? MouseButtonDown;

        /// <summary>
        /// Event raised when a mouse button has been released.
        /// </summary>
        event MouseButtonUpEventHandler? MouseButtonUp;

        /// <summary>
        /// Event raised when the mouse wheel has moved.
        /// </summary>
        event MouseWheelEventHandler? MouseWheelMoved;
    }
}
