using Finantech.Solutions.Core.Builder.Interfaces;
using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Builder;

public class PdfReportBuilder : IReportBuilder
{
    private Report _report;

    public IReportBuilder Initialize(Report baseReport)
    {
        _report = baseReport;
        return this;
    }
    public IReportBuilder ApplyFormatLayout()
    {
        if (_report != null)
        {
            _report.Format = "PDF";
            _report.Title = $"[PDF] {_report.Title}";
            _report.Content = $"[Formato_PDF] {_report.Content}";
        }
        return this;
    }

    public Report Build()
    {
       return _report ?? throw new InvalidOperationException("El reporte no ha sido inicializado.");
    }
}
