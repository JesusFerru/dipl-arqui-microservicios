namespace DesignPatterns.Adapter.Ejemplo
{
    public class CompanyBAdapter : IWebhookAdapter
    {
        private readonly CompanyBWebhookService _webhookService;
        private readonly string _oauthToken;

        public CompanyBAdapter(CompanyBWebhookService webhookService, string oauthToken)
        {
            _webhookService = webhookService;
            _oauthToken = oauthToken;
        }

        public void ReceiveNotification(string payload)
        {
            _webhookService.ConnectWithOAuth2(payload, _oauthToken);
        }
    }
}
