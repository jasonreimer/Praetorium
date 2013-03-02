using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Praetorium.Collections
{
    public class FunctionKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
    {

        private readonly Func<TItem, TKey> _keyForItem;

        public FunctionKeyedCollection(Func<TItem, TKey> keyForItem)
        {
            Ensure.ArgumentNotNull(() => keyForItem, ref _keyForItem);
        }

        public FunctionKeyedCollection(Func<TItem, TKey> keyForItem, IEnumerable<TItem> items)
            : this(keyForItem)
        {
            items.ForEach(Add);
        }

        protected override TKey GetKeyForItem(TItem item)
        {
            return _keyForItem(item);
        }

    }
}
