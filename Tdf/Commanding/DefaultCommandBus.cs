using Tdf.Dependency;
using Tdf.Domain.Uow;

namespace Tdf.Commanding
{
    public class DefaultCommandBus : ICommandBus
    {
        public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            try
            {
                var unitOfWork = UnitOfWorkContext.StartUnitOfWork();

                var executor = ObjectContainer.Resolve<ICommandExecutor<TCommand>>();
                executor.Execute(cmd);

                UnitOfWorkContext.Commit();
            }
            finally
            {
                UnitOfWorkContext.Close();
            }
        }
    }
}
