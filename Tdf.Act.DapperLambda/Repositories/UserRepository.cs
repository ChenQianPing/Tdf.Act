using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;

namespace Tdf.Act.DapperLambda.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User FirstOrDefault(Expression<Func<User, bool>> predicate)
        {
            using (var context = DBHelper.GetContext())
            {
                var user = context.Select<User>().Where(predicate).QuerySingle();
                return user;
            }
        }

        public User Insert(User entity)
        {
            using (var context = DBHelper.GetContext())
            {
                context.Insert(entity).Execute();
                return entity;
            }
        }

        public bool IsExist(Expression<Func<User, bool>> predicate)
        {
            using (var context = DBHelper.GetContext())
            {
                var user = context.Select<User>().Where(predicate).QuerySingle();
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public User Update(User entity)
        {
            using (var context = DBHelper.GetContext())
            {
                context.Update(entity).Execute();
                return entity;
            }
        }
    }
}
