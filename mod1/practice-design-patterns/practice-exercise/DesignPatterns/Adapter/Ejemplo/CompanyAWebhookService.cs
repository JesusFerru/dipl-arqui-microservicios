namespace DesignPatterns.Adapter.Ejemplo
{
    public class CompanyAWebhookService
    {
        public void ConnectWithApiToken( string data, string apiToken)
        {
            Console.WriteLine($"Empresa A valida token {apiToken}");
        }
    }
}
