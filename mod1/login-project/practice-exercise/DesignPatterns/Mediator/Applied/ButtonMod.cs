namespace DesignPatterns.Mediator.Applied
{
    public class ButtonMod
    {
        private IMediator _mediator;
        public ButtonMod(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Click()
        {
            Console.WriteLine("Button clicked.");
            ValidateClick("Right Click");
        }

        private void ValidateClick(string ev)
        {
            if (ev == "Right Click")
            {
                Console.WriteLine("Right button clicked.");
                _mediator.notify(this, "Right Click");
            }
            else
            {
                Console.WriteLine("Other button clicked.");
                _mediator.notify(this, "Click");
            }
        }
    }
}
