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

    // Formato (PDF, Excel, CSV, Others)
    public string Format { get; set; } = string.Empty;

    public string Export()
    {
        throw new NotImplementedException();
    }
}
