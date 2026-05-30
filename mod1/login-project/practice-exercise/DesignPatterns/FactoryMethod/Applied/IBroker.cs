using DesignPatterns.FactoryMethod;

namespace DesignPatterns.FactoryMethod.Applied
{
    public abstract class IBroker
    {
        public abstract INotification createNotification();
    }
}
