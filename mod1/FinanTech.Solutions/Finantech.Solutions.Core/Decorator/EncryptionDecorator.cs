using Finantech.Solutions.Core.Models;

namespace Finantech.Solutions.Core.Decorator;
public class EncryptionDecorator : ReportDecorator
{
    public EncryptionDecorator(IReportComponent report) : base(report) { }

    public override string Export()
    {
        string baseOutput = base.Export();

        // ANSI Escape Codes for coloring specific text blocks
        string yellowAnsi = "\x1b[38;5;178m"; // Mostaza
        string resetAnsi = "\x1b[0m";

        // Mocking encryption
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(baseOutput);
        string encryptedData = Convert.ToBase64String(bytes);

        return baseOutput +
            $"\n{yellowAnsi}==================================================\n{resetAnsi}" +
               $"{yellowAnsi}<<< CONTENIDO CIFRADO CON ALGORITMO AES-256 >>>\n{resetAnsi}" +
               $"{yellowAnsi}==================================================\n{resetAnsi}" +
               encryptedData +
               $"{yellowAnsi}\n=================================================={resetAnsi}";
    }
}