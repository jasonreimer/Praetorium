namespace Praetorium
{
    public interface INamedItem<T>
    {
        T Name { get; }
    }

    public interface INamedItem : INamedItem<string>
    {
    }
}
