using System.Collections.Generic;
using System.Linq;

namespace $rootnamespace$
{
    public interface IQueryProvider
    {
        IQueryable<T> Query<T>() where T : class;
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
    }
}