namespace DesignPatterns.Mediator.Applied
{
    public interface IMediator
    {
        void notify(object sender, string ev);
    }
}
