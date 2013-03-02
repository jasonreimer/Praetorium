using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Praetorium.Collections
{
    [Serializable]
    public class NamedItemCollection<TName, TItem> : KeyedCollection<TName, TItem> where TItem : INamedItem<TName>
    {

        #region Constructors

        public NamedItemCollection()
            : this(null)
        {
        }

        public NamedItemCollection(IEnumerable<TItem> items)
        {
            if (items != null)
                items.ForEach(Add);
        }

        #endregion

        #region Methods

        protected override TName GetKeyForItem(TItem item)
        {
            return item.Name;
        }

        #endregion

    }
}
