namespace DesignPatterns.Mediator.Applied
{
    public class Chat : IMediator
    {
        public TextBoxMod TextBox { get; set; }
        public ButtonMod Button { get; set; }
        public void notify(object sender, string ev)
        {
            if (ev == "Click" && sender is ButtonMod)
            {
                TextBox.Clean();
            }
            else if(ev == "Text Changed" && sender is TextBoxMod)
            {
                Console.WriteLine("User updated text");
            }
        }
    }
}
