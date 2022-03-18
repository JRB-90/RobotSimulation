namespace JSim.Core.Common
{
    /// <summary>
    /// Standard implementation of a message collator.
    /// </summary>
    public class MessageCollator : IMessageCollator
    {
        public MessageCollator()
        {
            handlers = new List<IMessageHandler>();
        }

        public bool ContainsHandlerFor<T>() where T : class
        {
            return
                handlers
                .OfType<IMessageHandler<T>>()
                .Count() > 0;
        }
        
        public void Subscribe(IMessageHandler messageHandler)
        {
            handlers.Add(messageHandler);
        }

        public void Unsubscribe(IMessageHandler messageHandler)
        {
            handlers.Remove(messageHandler);
        }

        public void Publish<T>(T message) where T : class
        {
            var messageHandlers = handlers.OfType<IMessageHandler<T>>();

            foreach (var messageHandler in messageHandlers)
            {
                messageHandler.Handle(message);
            }
        }

        private List<IMessageHandler> handlers;
    }
}
