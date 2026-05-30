using Finantech.Solutions.Core.Builder.Interfaces;
using Finantech.Solutions.Core.Factory;
using Finantech.Solutions.Core.Models.Enums;
using Finantech.Solutions.Core.Models;
using Finantech.Solutions.Core.Strategy.Intefaces;
using FinanTech.Solutions.App.Handler;
using Finantech.Solutions.Core.Decorator;

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
        Report structuralReport = reportBuilder
                                .Initialize(processedReport)
                                .ApplyFormatLayout()
                                .Build();

        Console.WriteLine($"-> [ÉXITO] Reporte generado en formato: {menuConfig.SelectedFormat}.");

        // PATRÓN DECORATOR: Business rules conditional execution
        IReportComponent finalReport = structuralReport;

        // Rule 1: HeaderDecorator ONLY for Executive users
        if (menuConfig.SelectedUserType == UserType.Executive)
        {
            finalReport = new HeaderDecorator(finalReport);
            Console.WriteLine("-> [DECORATOR AUTOMÁTICO] Aplicado Encabezado por perfil Ejecutivo.");
        }

        // Manual Option: Watermark (Always available)
        if (menuConfig.ApplyWatermark)
        {
            finalReport = new WatermarkDecorator(finalReport);
            Console.WriteLine("-> [DECORATOR MANUAL] Capa de Marca de Agua aplicada.");
        }

        // Rule 2: EncryptionDecorator ONLY if the report contains encrypted data (like Auditor)
        if (!string.IsNullOrEmpty(structuralReport.EncryptedCode))
        {
            finalReport = new EncryptionDecorator(finalReport);
            Console.WriteLine("-> [DECORATOR AUTOMÁTICO] Contenido cifrado por requerimiento de seguridad.");
        }

        // Manual Option: Compression (Always available)
        if (menuConfig.ApplyCompression)
        {
            finalReport = new CompressionDecorator(finalReport);
            Console.WriteLine("-> [DECORATOR MANUAL] Capa de Compresión ZIP aplicada.");
        }

        // OUTPUT: Display final structured output report
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nRESULTADO DEL REPORTE:");
        Console.ResetColor();
        Console.WriteLine(finalReport.Export());

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();

        var deliveryChannel = MenuHandler.SendFinalReport();
        menuConfig.SelectChannel = deliveryChannel;

        var deliveryStrategy = DeliveryStrategyFactory.GetStrategy(menuConfig.SelectChannel);

       
        deliveryStrategy.Deliver(finalReport.Export(), menuConfig.SelectChannel.ToString());
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