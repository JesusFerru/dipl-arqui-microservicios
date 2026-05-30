
namespace DesignPatterns.Adapter.Ejemplo
{
    public class CompanyCWebhookService
    {
        public void ConnectWithUserAndPassword(string data, string user, string password)
        {
            Console.WriteLine($"Empresa C valida user: {user} y password: {password}");
        }
    }
}
