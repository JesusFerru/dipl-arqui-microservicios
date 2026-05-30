using Finantech.Solutions.Core.Builder.Interfaces;
using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Builder;

public class ExcelReportBuilder : IReportBuilder
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
            _report.Format = "Excel (XLSX)";
            // Simula la inyección de estructura matricial de Excel
            _report.Title = $"[EXCEL-WORKBOOK] {_report.Title}";
            _report.Content = $"Sheet1 -> Row 1, Col 1: {_report.Content}";
            _report.Conclusions = $"Sheet2 (Summary) -> {_report.Conclusions}";
        }
        return this;
    }

    public Report Build()
    {
        return _report ?? throw new System.InvalidOperationException("Report was not initialized.");
    }
}
