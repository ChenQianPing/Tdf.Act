using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;
using Tdf.Domain.Repositories;

namespace Tdf.Act.EntityFramework.Repositories
{
    public class UserRepository : EfRepositoryBase<User>, IUserRepository
    {
        private ActDbContext _dbContext;

        public UserRepository(ActDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
