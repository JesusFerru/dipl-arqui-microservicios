using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethod.Applied
{
    public class SMSNotificator : IBroker
    {
        public override INotification createNotification()
        {
            return new SMSNotification();
        }
    }
}
