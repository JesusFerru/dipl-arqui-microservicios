using System.Text;

namespace Finantech.Solutions.Core.Models;

public class Report : IReportComponent
{
    // Attributes and properties
    public string Title { get; set; } = string.Empty;
    public string EncryptedCode { get; set; } = string.Empty;
    public DateTime GenerationDate { get; set; } = DateTime.Now;
    public string Content { get; set; } = string.Empty;
    public string DetailedInformation { get; set; } = string.Empty;
    public List<string> ChartsList { get; set; } = [];
    public string Conclusions { get; set; } = string.Empty;
    public string ResponsibleSignature { get; set; } = string.Empty;
    public List<string> Annexes { get; set; } = [];

    // Format (PDF, Excel, CSV, Others)
    public string Format { get; set; } = string.Empty;

    public string Export()
    {
        var showReport = new StringBuilder();
        showReport.AppendLine($"=== REPORT FORMAT: {Format} ===\n");
        showReport.AppendLine($"Reporte: {Title}");
        if (!string.IsNullOrEmpty(EncryptedCode)) showReport.AppendLine($"Codigo: {EncryptedCode}");
        showReport.AppendLine($"Fecha: {GenerationDate:yyyy-MM-dd}");
        showReport.AppendLine($"Contenido: {Content}");

        if (!string.IsNullOrEmpty(DetailedInformation)) showReport.AppendLine($"Info detallada: {DetailedInformation}");
        if (ChartsList.Count > 0) showReport.AppendLine($"Listado de gráficos: \n - {string.Join("\n - ", ChartsList)}");

        showReport.AppendLine($"Conclusiones: {Conclusions}");
        showReport.AppendLine($"Firma Responsable: {ResponsibleSignature}");

        if (Annexes.Count > 0) showReport.AppendLine($"Anexos: \n - {string.Join("\n - ", Annexes)}");
        showReport.AppendLine("=================================");

        return showReport.ToString();
    }
}
