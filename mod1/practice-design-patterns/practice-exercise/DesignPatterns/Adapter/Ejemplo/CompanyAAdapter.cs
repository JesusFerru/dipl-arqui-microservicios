
namespace DesignPatterns.Adapter.Ejemplo
{
    public class CompanyAAdapter : IWebhookAdapter
    {
        private readonly CompanyAWebhookService _webhookService;
        private readonly string _apiToken;

        public CompanyAAdapter(CompanyAWebhookService webhookService, string apiToken)
        {
            _webhookService = webhookService;
            _apiToken = apiToken;
        }

        public void ReceiveNotification(string payload)
        {
            _webhookService.ConnectWithApiToken(_apiToken, payload);
        }
    }
}

