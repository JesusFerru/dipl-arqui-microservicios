
namespace DesignPatterns.Adapter.Ejemplo
{
    public class CompanyBWebhookService
    {
        public void ConnectWithOAuth2(string data, string bearerToken)
        {
            Console.WriteLine($"Empresa B valida Bearer token {bearerToken}");
        }
    }
}
