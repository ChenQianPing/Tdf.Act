using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Domain.Repositories;
using Tdf.Domain.Uow;
using System.Linq.Expressions;

namespace Tdf.Nhb.Repositories
{
    public class NhbRepositoryBase<T> : IRepository<T>
        where T : class
    {
        public NhbUnitOfWork UnitOfWork { get; private set; }

        public ISession Session
        {
            get
            {
                return UnitOfWork.Session;
            }
        }

        public NhbRepositoryBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = (NhbUnitOfWork)unitOfWork;
        }

        public T Insert(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        public T Update(T entity)
        {
            Session.Update(entity);
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var entry = GetAll().Where(predicate);
            return (entry.Any());
        }
    }
}
