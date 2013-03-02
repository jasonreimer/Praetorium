using System;

namespace Praetorium.Services
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class DefaultEnpointNameAttribute : Attribute
    {
        private readonly string _name;

        public DefaultEnpointNameAttribute(string name)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => name, ref _name);
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
