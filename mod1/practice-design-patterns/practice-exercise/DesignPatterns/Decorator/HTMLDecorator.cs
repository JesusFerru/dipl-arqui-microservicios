namespace DesignPatterns.Decorator
{
    public class HTMLDecorator : MessageDecorator
    {
        public HTMLDecorator(IMessage message): base(message) { }

        public override void Send(string message)
        {
            Console.WriteLine("<html>");
            Console.WriteLine("<body>");
            base.Send(message);
            Console.WriteLine("</html>");
            Console.WriteLine("</body>");
        }
    }
}
