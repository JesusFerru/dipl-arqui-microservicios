using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Decorator;

public class WatermarkDecorator : ReportDecorator
{
    public WatermarkDecorator(IReportComponent report) : base(report) { }

    public override string Export()
    {
        string baseOutput = base.Export();
        // ANSI Escape Codes for coloring specific text blocks
        string orangeAnsi = "\x1b[38;5;208m"; // Switches console output to Orange Color
        string resetAnsi = "\x1b[0m";

        string watermarkBlock = $"\n{orangeAnsi}[MARCA DE AGUA: FINTECH SOL 2026 - PROPIEDAD INTELECTUAL NO DISTRIBUIR]{resetAnsi}";
        return baseOutput + watermarkBlock;
    }
}