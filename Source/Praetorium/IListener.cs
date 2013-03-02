namespace Praetorium
{
    public interface IListener
    {
    }

    public interface IListener<TMessage> : IListener where TMessage : class
    {
        void Handle(TMessage message);
    }
}
