namespace Praetorium.Contexts
{
    public interface IContext
    {

        object this[string key]
        {
            get;
            set;
        }

        void Add(string key, object value);

        bool Contains(string key);

        void Remove(string key);

    }
}
