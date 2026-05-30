using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethod
{
    public class SMSNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Hello, from SMS: {message}");
        }
    }
}
