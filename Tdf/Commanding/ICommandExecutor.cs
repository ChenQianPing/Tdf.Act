namespace Tdf.Commanding
{
    public interface ICommandExecutor<TCommand>
        where TCommand : ICommand
    {
        void Execute(TCommand cmd);
    }
}
