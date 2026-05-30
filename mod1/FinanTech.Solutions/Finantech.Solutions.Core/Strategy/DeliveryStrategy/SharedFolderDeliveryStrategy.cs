using Finantech.Solutions.Core.Strategy.Intefaces;

namespace Finantech.Solutions.Core.Strategy.DeliveryStrategy;
public class SharedFolderDeliveryStrategy : IDeliveryStrategy
{
    public void Deliver(string reportContent, string destination)
    {
        Console.WriteLine("\n\x1b[38;5;178m[CANAL - ONEDRIVE FOLDER] Verificando permisos de red en volumen compartido...\x1b[0m");
        Console.WriteLine($"\x1b[38;5;178m[CANAL - ONEDRIVE FOLDER] Archivo guardado correctamente en: OneDrive\\{destination}\\Reporte_Finantech.txt\x1b[0m");
    }
}