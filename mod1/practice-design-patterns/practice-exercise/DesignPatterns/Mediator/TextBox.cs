namespace DesignPatterns.Mediator
{
    public class TextBox
    {
        public required Button Button { get; set; }
        public required Line Line { get; set; }
    
        public void Clean()
        {
            // Limpia el contenido del TextBox
            Console.WriteLine("TextBox cleaned.");
        }
        public void Write(string text)
        {
            Line.Update(text);
        }
    }
}
