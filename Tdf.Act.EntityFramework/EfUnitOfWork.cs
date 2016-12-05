using System;
using System.Threading.Tasks;
using Tdf.Domain.Uow;

namespace Tdf.Act.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private ActDbContext _dbContext;

        public EfUnitOfWork(ActDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
