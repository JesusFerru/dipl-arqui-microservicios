using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Decorator;
public class CompressionDecorator : ReportDecorator
{
    public CompressionDecorator(IReportComponent report) : base(report) { }

    public override string Export()
    {
        string baseOutput = base.Export();
        // ANSI Escape Codes for coloring specific text blocks
        string grayAnsi = "\x1b[38;5;242m"; // Gris
        string resetAnsi = "\x1b[0m";


        return "--------------------------------------------------\n" +
               $"{grayAnsi}[EMPAQUE COMPRIMIDO (.ZIP) - TAMAÑO REDUCIDO AL 35%]\n{resetAnsi}" +
               "--------------------------------------------------\n" +
               baseOutput +
               "\n--------------------------------------------------";
    }
}
