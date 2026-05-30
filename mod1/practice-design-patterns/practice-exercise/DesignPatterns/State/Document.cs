using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.State
{
    public class Document
    {
        private string _state = "draft";
        public void Edit()
        {
            if(_state == "draft")
            {
                Console.WriteLine("Editing document...");
            }
            else
            {
                Console.WriteLine("Cannot edit document in this state.");
            }
        }

        public void Publish()
        {
            if (_state == "draft")
            {
                _state = "review";
                Console.WriteLine("Document sent to review.");
            }
            else
            {
                Console.WriteLine("Cannot publish document in this state.");
            }
        }
    }
}
