namespace DesignPatterns.Adapter.Ejemplo
{
    public interface IWebhookAdapter
    {
        void ReceiveNotification(string payload);
    }
}
