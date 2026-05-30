using DesignPatterns.FactoryMethod.Applied;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DesignPatterns.FactoryMethod
{
    public class App
    {
        static void Main(string[] args)
        {
            string type = "Email";

            //INotification notification;

            //if (type == "Email")
            //{
            //    notification = new EmailNotification();
            //}
            //else
            //{
            //    notification = new SMSNotification();

            //}

            ////////////////////
            ///
            IBroker broker = new SMSNotificator();

            INotification notification = broker.createNotification();


            notification.Send($"Hola via {type}");
        }
    }
}
