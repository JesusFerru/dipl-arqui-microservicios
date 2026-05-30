namespace Finantech.Solutions.Core.Models;

public interface IReportComponent
{
    string Title { get; }
    string EncryptedCode { get; }
    DateTime GenerationDate { get; }
    string Content { get; }
    string DetailedInformation { get; }
    List<string> ChartsList { get; }
    string Conclusions { get; }
    string ResponsibleSignature { get; }
    List<string> Annexes { get; }
    string Format { get; }

    string Export();
}
