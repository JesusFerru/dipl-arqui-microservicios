using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Strategy.Intefaces;
/// <summary>
///  Interfaz para aplicar el patron Strategy para los diferentes tipos de procesamiento
///  Nivel de detalle: Ejecutivo, Analista, Auditor
/// </summary>
public interface IProcessingStrategy
{
    Report ProcessData(FinancialData data);
}