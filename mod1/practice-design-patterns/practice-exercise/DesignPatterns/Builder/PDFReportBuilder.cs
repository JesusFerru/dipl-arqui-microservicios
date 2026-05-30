

namespace DesignPatterns.Builder
{
    public class PDFReportBuilder : IBuilder
    {
        private PDFReport _report;

        public void DownloadAudioReport()
        {
            Console.WriteLine("Se puede descargar el reporte");
        }

        public void Reset()
        {
     //       _report = new PDFReport();
        }

        public void SetAuthor(string author)
        {
            _report.SetAuthor(author);
        }

        public void SetContent(string content)
        {
            _report.SetContent(content);
        }

        public void SetCreationDate(DateTime creationDate)
        {
            _report.SetCreationDate(creationDate);
        }

        public void SetFooter(string footer)
        {
            _report.SetFooter(footer);
        }

        public void SetTitle(string title)
        {
            _report.SetTitle(title);
        }
    }
}
