
using System.Threading.Tasks;

namespace DesignPatterns.State.Applied
{
    public class ReviewState : IDocumentState
    {
        public void Edit(DocumentContext context)
        {
            Console.WriteLine("Review: cannot edit in this state");
        }
        public void Publish(DocumentContext context)
        {
            Console.WriteLine("Document published");
            context.SetState(new PublishState());
        }
    }
}
