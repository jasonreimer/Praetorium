using System;

namespace Praetorium.Configuration
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class DefaultSectionNameAttribute : Attribute
    {

        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSectionNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="name"/> is null or empty.
        /// </exception>
        public DefaultSectionNameAttribute(string name)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => name, ref _name);
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
