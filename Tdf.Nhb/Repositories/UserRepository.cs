using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;
using Tdf.Domain.Uow;

namespace Tdf.Nhb.Repositories
{
    public class UserRepository : NhbRepositoryBase<User>, IUserRepository
    {
        public NhbUnitOfWork _UnitOfWork { get; private set; }

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _UnitOfWork = (NhbUnitOfWork)unitOfWork;
        }
    }
}
