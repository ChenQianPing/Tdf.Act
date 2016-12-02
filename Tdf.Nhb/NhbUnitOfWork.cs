using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Domain.Uow;

namespace Tdf.Nhb
{
    public class NhbUnitOfWork : IUnitOfWork
    {
        public ISession Session { get; private set; }

        public NhbUnitOfWork(ISession session)
        {
            Session = session;
            Session.BeginTransaction();
        }

        public void Commit()
        {
            Session.Transaction.Commit();
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}

