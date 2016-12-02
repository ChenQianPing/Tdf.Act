using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Domain.Uow;

namespace Tdf.Act.DapperLambda
{
    public class DapperLambdaUnitOfWork : IUnitOfWork
    {
        public void Commit() { }
        
        public void Dispose() { }
        
    }
}
