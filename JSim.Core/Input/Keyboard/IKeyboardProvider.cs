namespace JSim.Core.Input
{
    /// <summary>
    /// Provides an abstraction for all key event generators.
    /// </summary>
    public interface IKeyboardProvider
    {
        /// <summary>
        /// Event fired when a key has been pressed.
        /// </summary>
        event KeyDownEventHandler? KeyDown;

        /// <summary>
        /// Event fired when a key has been released.
        /// </summary>
        event KeyUpEventHandler? KeyUp;
    }
}
