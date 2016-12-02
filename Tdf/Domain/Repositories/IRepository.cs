using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tdf.Domain.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        //IQueryable<T> GetAll();
        T Insert(T entity);
        T Update(T entity);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        bool IsExist(Expression<Func<T, bool>> predicate);

    }
}
