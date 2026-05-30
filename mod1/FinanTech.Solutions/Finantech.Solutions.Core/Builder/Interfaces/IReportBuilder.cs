using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Builder.Interfaces;

// Inferfaz Builder paral configurar los formatos y estructuras del reporte en pasos
public interface IReportBuilder
{
    IReportBuilder Initialize(Report baseReport);
    IReportBuilder ApplyFormatLayout();
    Report Build();
}
