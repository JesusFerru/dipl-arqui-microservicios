using Finantech.Solutions.Core.Models.Enums;
using Finantech.Solutions.Core.Strategy.Intefaces;
using Finantech.Solutions.Core.Strategy.ProcessingStrategy;

namespace Finantech.Solutions.Core.Factory;

/// <summary>
/// Se aplicará Patrón Factory para crear instancias de las estrategias de procesamiento (Ejecutivo, Analista, Auditor) 
/// según el tipo de usuario o requerimiento específico.
/// </summary>
public static class ProcessingStrategyFactory
{
    public static IProcessingStrategy GetStrategy(UserType userType)
    {
        return userType switch
        {
            UserType.Executive => new ExecutiveProcessingStrategy(),
            UserType.Auditor => new AuditorProcessingStrategy(),
            UserType.Analyst => new AnalystProcessingStrategy(),
            _ => throw new ArgumentException($"Tipo de usuario {1} no definido", nameof(userType))
        };
    }
}
