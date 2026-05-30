using Finantech.Solutions.Core.Builder.Interfaces;
using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Builder;
public class CsvReportBuilder : IReportBuilder
{
    private Report? _report;

    public IReportBuilder Initialize(Report baseReport)
    {
        _report = baseReport;
        return this;
    }

    public IReportBuilder ApplyFormatLayout()
    {
        if (_report != null)
        {
            _report.Format = "CSV";
            // Simula un formato plano plano delimitado por comas
            _report.Title = $"CSV_FORMAT_{_report.Title.Replace(" ", "_")}";
            _report.Content = $"\"TITLE\",\"CONTENT\"\n\"{_report.Title}\",\"{_report.Content}\"";
        }
        return this;
    }

    public Report Build()
    {
        return _report ?? throw new System.InvalidOperationException("Report was not initialized.");
    }
}
