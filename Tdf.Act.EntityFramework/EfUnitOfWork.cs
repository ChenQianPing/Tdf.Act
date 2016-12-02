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

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
