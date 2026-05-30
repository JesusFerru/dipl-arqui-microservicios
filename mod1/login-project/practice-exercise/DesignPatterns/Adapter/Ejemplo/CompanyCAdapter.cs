namespace DesignPatterns.Adapter.Ejemplo
{
    public class CompanyCAdapter : IWebhookAdapter
    {
        private readonly CompanyCWebhookService _webhookService;
        private readonly string _user;
        private readonly string _password;

        public CompanyCAdapter(CompanyCWebhookService webhookService, string user, string password)
        {
            _webhookService = webhookService;
            _user = user;
            _password = password;
        }

        public void ReceiveNotification(string payload)
        {
            _webhookService.ConnectWithUserAndPassword(payload, _user, _password);
        }
    }
}
