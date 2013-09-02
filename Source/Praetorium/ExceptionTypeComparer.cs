using System;
using System.Collections.Generic;

namespace Praetorium
{
    public class ExceptionTypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            Ensure.ArgumentNotNull(() => x);
            Ensure.ArgumentNotNull(() => y);

            if (x.Equals(y))
                return 0;

            if (x.DerivesFrom(y))
                return 1;

            if (y.DerivesFrom(x))
                return -1;

            return 0;
        }
    }
}
