using Finantech.Solutions.Core.Models;
using Finantech.Solutions.Core.Strategy.Intefaces;

namespace Finantech.Solutions.Core.Strategy.ProcessingStrategy;

public class AuditorProcessingStrategy : IProcessingStrategy
{
    public Report ProcessData(FinancialData data)
    {
        return new Report
        {
            Title = "Auditoría financiera encriptada",
            Content = "Contenido estandar: Estado general de las finanzas, con enfoque en auditoría y cumplimiento normativo. ",
            DetailedInformation = GetDetailedInformation(data),
            EncryptedCode = EncryptData(DateTime.Now.Ticks.ToString()),
            Conclusions = "Sin anomalías mayores detectadas. Mantener controles internos actuales",
            ResponsibleSignature = "Certified Internal Auditor (CIA)",
            Annexes = new List<string> { "Anexo A: Facturas Proveedores adjuntas", "Anexo B: Estados financieros." }
        };
    }

    private string GetDetailedInformation(FinancialData data)
    {
        return "(Datos procesados: \" + data.RawDataSummary + \")\n\n " +
            "LOGS DE AUDITORÍA CRÍTICOS: " +
            "\n - Transacciones sospechosas detectadas en el mes de marzo. " +
            "\n - Discrepancias menores en los balances de cierre trimestrales. " +
            "\n - Cumplimiento normativo: 95% (3 incumplimientos menores identificados).";
    }

    private string EncryptData(string input)
    {
        // Simulación de encriptación (en un caso real, se debe usar un algoritmo de encriptación supervisado)
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }
}
