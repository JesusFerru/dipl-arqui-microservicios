using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Decorator;

public class HeaderDecorator : ReportDecorator
{
    public HeaderDecorator(IReportComponent report) : base(report) { }

    public override string Export()
    {
        string baseOutput = base.Export();

        // ANSI Escape Codes for coloring specific text blocks
        string darkCyanAnsi = "\x1b[36m"; // Switches console output to Blue Color
        string resetAnsi = "\x1b[0m";

        string headerBlock = $"{darkCyanAnsi}--- ENCABEZADO CORPORATIVO: FinanTech Solutions Co. ---\n" +
                                 $"--- CLASIFICACIÓN: CONFIDENCIAL E INTERNO -----------\n{resetAnsi}";

        return headerBlock + baseOutput;
    }
}
