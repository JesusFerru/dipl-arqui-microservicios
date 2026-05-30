namespace DesignPatterns.State.Applied
{
    public interface IDocumentState
    {
        void Edit(DocumentContext context);
        void Publish(DocumentContext context);
    }
}
