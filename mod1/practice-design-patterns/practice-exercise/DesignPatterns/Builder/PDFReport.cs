namespace DesignPatterns.Builder
{
    public class PDFReport
    {
        public PDFReport(string title, string content, string author, string footer, DateTime creationDate)
        {
            Title = title;
            Content = content;
            Author = author;
            Footer = footer;
            CreationDate = creationDate;
        }

        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public string? Footer { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public void SetTitle(string title) => Title = title;
        public void SetContent(string content) => Content = content;
        public void SetAuthor(string author) => Author = author;
        public void SetFooter(string footer) => Footer = footer;
        public void SetCreationDate(DateTime creationDate) => CreationDate = creationDate;

        public override string ToString()
        {
            return $"Title: {Title} \n Content: {Content} \n Footer: {Footer}";
        }
    }

    // validación

}
