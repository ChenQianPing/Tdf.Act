using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
