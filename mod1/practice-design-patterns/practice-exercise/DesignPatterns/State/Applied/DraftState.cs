using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.State.Applied
{
    public class DraftState : IDocumentState
    {
        public void Edit(DocumentContext context)
        {
            Console.WriteLine("Now you can edit the document");
        }
        public void Publish(DocumentContext context)
        {
            Console.WriteLine("Document is now in review state.");
            context.SetState(new ReviewState());
        }
    }
}
