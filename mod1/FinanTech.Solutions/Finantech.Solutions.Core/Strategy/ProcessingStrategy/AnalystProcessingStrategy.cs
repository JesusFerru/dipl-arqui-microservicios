using Finantech.Solutions.Core.Models;
using Finantech.Solutions.Core.Strategy.Intefaces;

namespace Finantech.Solutions.Core.Strategy.ProcessingStrategy;

public class AnalystProcessingStrategy : IProcessingStrategy
{
    public Report ProcessData(FinancialData data)
    {
        return new Report
        {
            Title = "Análisis de mercado según indicadores financieros",
            Content = "Analisis estructural de metadatos obtenidos del mercado",
            DetailedInformation = GetDetailedInformation(data),
            ChartsList = new List<string> { "Graf 1: Curva de crecimiento de ingresos ", "Graf 2: Distribución de Gastos:", "Graf 3: ROI Heatmap" },
            Conclusions = "Indicadores de mercado sugieren un crecimiento sostenido en el próximo trimestre, con oportunidades significativas en el sector tecnológico. Se recomienda aumentar la inversión en activos relacionados con la innovación y diversificar el portafolio para mitigar riesgos.",
            ResponsibleSignature = "Analista Senior: Luis Jesús Ferrufino",
            Annexes = new List<string> { "I: Supuestos macroeconómicos", " II: Comparativa de Datos Históricos" }
            // No incluye código encriptado
        };
    }

    private string GetDetailedInformation(FinancialData data)
    {
        return "(Datos procesados: " + data.RawDataSummary + ")\n\n " +
            "ANÁLISIS DETALLADO: " +
            "\n - Proyección de crecimiento basada en tendencias históricas y condiciones actuales del mercado. " +
            "\n - Evaluación de riesgos potenciales asociados con la volatilidad del mercado. " +
            "\n - Recomendaciones estratégicas para capitalizar oportunidades emergentes.";
    }
}
