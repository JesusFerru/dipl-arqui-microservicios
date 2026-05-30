using Finantech.Solutions.Core.Models;
using Finantech.Solutions.Core.Strategy.Intefaces;
namespace Finantech.Solutions.Core.Strategy.ProcessingStrategy;
public class ExecutiveProcessingStrategy : IProcessingStrategy
{
    public Report ProcessData(FinancialData data)
    {
        return new Report
        {
            Title = "Indicadores Financieros para Ejecutivos",
            Content = "Resumen: Estado: Alto nivel. Operaciones: Estables (Datos procesados: " + data.RawDataSummary + ")",
            Conclusions = "La empresa mantiene un flujo de dinero saludable. Se recomienda proceder con los planes de inversión.",
            ResponsibleSignature = "Cómite Ejecutivo"
        };
    }
}