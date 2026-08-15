namespace DesignPatterns.Decorator
{
    public abstract class MessageDecorator : IMessage
    {
        protected IMessage _message;

        public MessageDecorator(IMessage message)
        {
            _message = message;
        }
        public virtual void Send(string message)
        {
            _message.Send(message);
        }
    }
}
