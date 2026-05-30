namespace DesignPatterns.Mediator
{
    public class Button
    {
        public required TextBox TextBox { get; set; }
        public required Line Line { get; set; }
        public void Click()
        {
            TextBox.Clean();
            Line.Update("From button");
        }
    }
}
