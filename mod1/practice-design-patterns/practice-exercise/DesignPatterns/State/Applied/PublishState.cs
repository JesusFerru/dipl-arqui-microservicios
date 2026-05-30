
namespace DesignPatterns.State.Applied
{
    public class PublishState : IDocumentState
    {
        public void Edit(DocumentContext context)
        {
            Console.WriteLine("Published: Cannot edit document in published state.");
        }

        public void Publish(DocumentContext context)
        {
            Console.WriteLine("Published: Document already publishd");
        }
 
    }
}
