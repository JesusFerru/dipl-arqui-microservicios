using Finantech.Solutions.Core.Builder.Interfaces;
using Finantech.Solutions.Core.Builder;

namespace Finantech.Solutions.Core.Factory;

public static class ReportBuilderFactory
{
    public static IReportBuilder GetBuilder(string format)
    {
        return format.ToUpper() switch
        {
            "PDF" => new PdfReportBuilder(),
            "EXCEL" => new ExcelReportBuilder(),
            "CSV" => new CsvReportBuilder(),
            _ => throw new ArgumentException($"Formato '{format}' no soportado", nameof(format))
        };
    }
}
