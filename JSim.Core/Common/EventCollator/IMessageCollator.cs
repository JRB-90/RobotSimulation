namespace JSim.Core.Common
{
    /// <summary>
    /// Message collator to provide a mechanism for pub/sub uncoupled events.
    /// </summary>
    public interface IMessageCollator
    {
        /// <summary>
        /// Checks to see if a message type has at least one handler subscribed.
        /// </summary>
        /// <typeparam name="T">Type of the message you want to check.</typeparam>
        /// <returns>True if message type has at least one handler subscribed.</returns>
        bool ContainsHandlerFor<T>() where T : class;

        /// <summary>
        /// Subscribes a message handler implementation to published messages.
        /// </summary>
        /// <param name="messageHandler">Message handler implementation for the type to subscribe to.</param>
        void Subscribe(IMessageHandler messageHandler);

        /// <summary>
        /// Unsubscribes a message handler implementation.
        /// </summary>
        /// <param name="messageHandler">Message handler implementation for the type to unsubscribe from.</param>
        void Unsubscribe(IMessageHandler messageHandler);

        /// <summary>
        /// Publishes a message to all subscribers of the message type.
        /// </summary>
        /// <typeparam name="T">Type of the message to publish.</typeparam>
        /// <param name="message">Message to publish to subscribers.</param>
        void Publish<T>(T message) where T : class;
    }
}
