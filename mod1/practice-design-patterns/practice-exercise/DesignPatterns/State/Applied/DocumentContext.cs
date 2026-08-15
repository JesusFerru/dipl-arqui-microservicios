namespace DesignPatterns.State.Applied
{
    public class DocumentContext
    {
        private IDocumentState _state;

        public DocumentContext(IDocumentState state)
        {
            _state = state;
        }
        public void SetState(IDocumentState state)
        {
            _state = state;
        }
        public void Edit()
        {
            _state.Edit(this);
        }

        public void Publish()
        {
            _state.Publish(this);
        }
    }
}
