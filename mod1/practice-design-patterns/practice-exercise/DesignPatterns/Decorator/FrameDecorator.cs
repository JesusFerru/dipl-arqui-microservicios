namespace DesignPatterns.Decorator
{
    public class FrameDecorator : MessageDecorator
    {
        public FrameDecorator(IMessage message) : base(message) { }

        public override void Send(string message)
        {
            Console.WriteLine("////////////////");
            base.Send(message); 
            Console.WriteLine("////////////////");

        }
    }
}
