using Finantech.Solutions.Core.Strategy.Intefaces;
using System;
namespace Finantech.Solutions.Core.Strategy.DeliveryStrategy;

public class EmailDeliveryStrategy : IDeliveryStrategy
{
    public void Deliver(string reportContent, string destination)
    {
        Console.WriteLine("\n\x1b[38;5;178m[CANAL - CORREO] Conectando al servidor SMTP corporativo...\x1b[0m");
        Console.WriteLine($"\x1b[38;5;178m[CANAL - CORREO] Enviando reporte exitosamente al {destination}: ljesusfb02@gmail.com\x1b[0m");
    }
}