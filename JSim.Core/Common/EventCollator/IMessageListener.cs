namespace JSim.Core.Common
{
    /// <summary>
    /// Convienience interface to denote all message handlers.
    /// </summary>
    public interface IMessageHandler
    {
    }

    /// <summary>
    /// Denotes that a class can handle a particular message type.
    /// Class must implement this interface for each type of message they want to subscribe to.
    /// </summary>
    /// <typeparam name="T">Type of message to subscribe to.</typeparam>
    public interface IMessageHandler<T> : IMessageHandler where T : class
    {
        void Handle(T message);
    }
}
