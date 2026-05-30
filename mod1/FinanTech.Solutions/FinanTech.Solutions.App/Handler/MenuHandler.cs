using Finantech.Solutions.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanTech.Solutions.App.Handler
{
    public static class MenuHandler
    {
        public static ReportMenuConfig ShowMainMenu()
        {
            var config = new ReportMenuConfig();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("    FINANTECH SOLUTIONS - SISTEMA DE REPORTES     ");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            Console.WriteLine("\n[Paso 1] Ingrese el resumen de datos financieros básicos: \n(Si no ingresa datos, se usará ejemplo)\n\n");
            Console.Write("> ");
            config.RawDataInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(config.RawDataInput))
            {
                config.RawDataInput = "Ingresos: $100K, Gastos: $80K, Ganancia: $20K";
                Console.WriteLine($"Datos Ejemplo: {config.RawDataInput} \n");
            }

            // Select User Type (Strategy)
            Console.WriteLine("\n[Paso 2] Seleccione el Tipo de Usuario para el procesamiento:");
            Console.WriteLine("1. Ejecutivo (Resumen Básico)");
            Console.WriteLine("2. Auditor (Encriptado)");
            Console.WriteLine("3. Analista (Detallado + Gráficos/Anexos)");
            Console.Write("Elija una opción (1-3): ");

            config.SelectedUserType = UserType.Executive;
            switch (Console.ReadLine())
            {
                case "1": config.SelectedUserType = UserType.Executive; break;
                case "2": config.SelectedUserType = UserType.Auditor; break;
                case "3": config.SelectedUserType = UserType.Analyst; break;
                default:
                    Console.WriteLine("Opción inválida. Usando Ejecutivo por defecto.");
                    break;
            }

            // Select Output Format (Builder)
            Console.WriteLine("\n[Paso 3] Seleccione el Formato de Salida del Reporte:");
            Console.WriteLine("1. PDF");
            Console.WriteLine("2. Excel");
            Console.WriteLine("3. CSV");
            Console.Write("Elija una opción (1-3): ");

            config.SelectedFormat = "PDF";
            switch (Console.ReadLine())
            {
                case "1": config.SelectedFormat = "PDF"; break;
                case "2": config.SelectedFormat = "EXCEL"; break;
                case "3": config.SelectedFormat = "CSV"; break;
                default:
                    config.SelectedFormat = "PDF";
                    break;
            }
            // Optional Improvements (Decorator)
            Console.WriteLine("\n[Paso 4] Seleccione las mejoras opcionales (S/N):");

            Console.Write("¿Agregar Marca de Agua de seguridad? (S/N): ");
            config.ApplyWatermark = Console.ReadLine()?.ToUpper() == "S";

            Console.Write("¿Aplicar Compresión final ZIP? (S/N): ");
            config.ApplyCompression = Console.ReadLine()?.ToUpper() == "S";

            return config;
        }
    }
}
