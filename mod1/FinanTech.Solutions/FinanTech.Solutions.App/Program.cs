using Finantech.Solutions.Core.Builder.Interfaces;
using Finantech.Solutions.Core.Factory;
using Finantech.Solutions.Core.Models.Enums;
using Finantech.Solutions.Core.Models;
using Finantech.Solutions.Core.Strategy.Intefaces;
using FinanTech.Solutions.App.Handler;

// Encerrar todo en un do while para finalizar el programa después de mostrar el resultado
int retryReport = 1;
do
{
    Console.Clear();


    ReportMenuConfig menuConfig = MenuHandler.ShowMainMenu();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n==================================================");
    Console.WriteLine("        GENERANDO REPORTE ...      ");
    Console.WriteLine("==================================================");
    Console.ResetColor();

    try
    {
        var financialData = new FinancialData { RawDataSummary = menuConfig.RawDataInput };
        // PATRÓN STRATEGY: Resolve and process data according to user role
        IProcessingStrategy processingStrategy = ProcessingStrategyFactory.GetStrategy(menuConfig.SelectedUserType);
        Report processedReport = processingStrategy.ProcessData(financialData);
        Console.WriteLine($"-> [ÉXITO] Datos procesados usando la estrategia de {menuConfig.SelectedUserType}.");

        // PATRÓN BUILDER: Resolve format and construct final structural layout
        IReportBuilder reportBuilder = ReportBuilderFactory.GetBuilder(menuConfig.SelectedFormat);
        Report finalReport = reportBuilder
                                .Initialize(processedReport)
                                .ApplyFormatLayout()
                                .Build();

        Console.WriteLine($"-> [ÉXITO] Reporte generado en formato: {menuConfig.SelectedFormat}.");

        // OUTPUT: Display final structured output report
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nRESULTADO DEL REPORTE:");
        Console.ResetColor();
        Console.WriteLine(finalReport.Export());
    }
    catch (Exception ex)
    {
        // Handle structural exceptions thrown by factories
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n[ERROR DE ARQUITECTURA] {ex.Message}");
        Console.ResetColor();
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();

    Console.WriteLine("\nDesea generar otro reporte?\n");
    Console.WriteLine("\n1. Generar otro reporte \n");
    Console.WriteLine("\n2. Salir del sistema\n");

    Console.Write("> ");
    retryReport = int.Parse(Console.ReadLine());


} while (retryReport == 1); 

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();