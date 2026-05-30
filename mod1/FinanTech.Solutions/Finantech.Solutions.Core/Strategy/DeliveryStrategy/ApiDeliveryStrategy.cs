using Finantech.Solutions.Core.Strategy.Intefaces;

namespace Finantech.Solutions.Core.Strategy.DeliveryStrategy;

internal class ApiDeliveryStrategy : IDeliveryStrategy
{
    public void Deliver(string reportContent, string destination)
    {
        Console.WriteLine("\n\x1b[38;5;178m[CANAL - API] Verificando permisos conexión API...\x1b[0m");
        Console.WriteLine($"\x1b[38;5;178m[CANAL - API] HTTP 200 OK. \n Enviado correctamente correctamente en: https://www.fintech-sol.org/\\{destination}\\export-report\x1b[0m");
    }
}