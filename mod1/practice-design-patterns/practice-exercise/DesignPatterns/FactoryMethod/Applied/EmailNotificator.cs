
namespace DesignPatterns.FactoryMethod.Applied
{
    public class EmailNotificator : IBroker
    {
        public override INotification createNotification()
        {
            return new EmailNotification();
        }
    }
}
