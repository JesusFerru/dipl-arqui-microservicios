namespace DesignPatterns.Mediator.Applied
{
    public class TextBoxMod
    {
        private IMediator _mediator;
        public TextBoxMod(IMediator mediator)
        {
            _mediator = mediator;
        }
        public void Clean()
        {
            Console.WriteLine("TextBox cleaned.");
            _mediator.notify(this, "Clean");
        }
        public void ChangeText(string text)
        {
            Console.WriteLine($"TextBox text changed to: {text}");
            _mediator.notify(this, "Text Changed");
        }
    }
}
