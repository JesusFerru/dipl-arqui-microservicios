namespace DesignPatterns.Builder
{
    public interface IBuilder
    {
        void SetTitle(string title);
        void SetContent(string content);
        void SetAuthor(string author);
        void SetFooter(string footer);
        void SetCreationDate(DateTime creationDate);
        void Reset();
        void DownloadAudioReport();
    }
}
