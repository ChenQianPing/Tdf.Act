using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Act.Domain.Entities;
using Tdf.Domain.Repositories;

namespace Tdf.Act.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
