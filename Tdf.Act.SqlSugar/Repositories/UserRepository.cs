using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;
using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;

namespace Tdf.Act.SqlSugar.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User FirstOrDefault(Expression<Func<User, bool>> predicate)
        {
            using (var db = SugarFactory.GetInstance())
            {
                // select single
                var single = db.Queryable<User>().Single(predicate);
                return single;
            }
        }

        public User Insert(User entity)
        {
            using (var db = SugarFactory.GetInstance())
            {
                // insert item
                db.Insert(entity);
                return entity;
            }
        }

        public bool IsExist(Expression<Func<User, bool>> predicate)
        {
            using (var db = SugarFactory.GetInstance())
            {
                // is any
                bool isAny = db.Queryable<User>().Any(predicate);
                return isAny;
            }
        }

        public User Update(User entity)
        {
            using (var db = SugarFactory.GetInstance())
            {
                // update by entity
                db.Update(entity);
                return entity;
            }
        }
    }
}
