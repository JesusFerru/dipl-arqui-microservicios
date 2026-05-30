using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Decorator;

// BASE DECORATOR: Implements contract and delegates execution to the wrapped component
public abstract class ReportDecorator : IReportComponent
{
    protected readonly IReportComponent _wrappedReport;

    protected ReportDecorator(IReportComponent report)
    {
        _wrappedReport = report;
    }

    public virtual string Title => _wrappedReport.Title;
    public virtual string EncryptedCode => _wrappedReport.EncryptedCode;
    public virtual DateTime GenerationDate => _wrappedReport.GenerationDate;
    public virtual string Content => _wrappedReport.Content;
    public virtual string DetailedInformation => _wrappedReport.DetailedInformation;
    public virtual List<string> ChartsList => _wrappedReport.ChartsList;
    public virtual string Conclusions => _wrappedReport.Conclusions;
    public virtual string ResponsibleSignature => _wrappedReport.ResponsibleSignature;
    public virtual List<string> Annexes => _wrappedReport.Annexes;
    public virtual string Format => _wrappedReport.Format;

    public virtual string Export()
    {
        return _wrappedReport.Export();
    }
}
