namespace DesignPatterns.Decorator
{
    public class GeneralMessage : IMessage
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }
}
