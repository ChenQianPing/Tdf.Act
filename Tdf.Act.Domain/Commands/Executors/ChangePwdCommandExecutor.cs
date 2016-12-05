using Tdf.Act.Domain.Repositories;
using Tdf.Act.Domain.Services;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class ChangePwdCommandExecutor : ICommandExecutor<ChangePwdCommand>
    {

        public IUserRepository _repository;

        public ChangePwdCommandExecutor(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangePwdCommand cmd)
        {
            var service = new UserService(_repository);
            var user = service.ChangePwd(cmd.UserId, cmd.OldPwd, cmd.NewPwd);

            cmd.ExecutionResult = new ChangePwdCommandResult
            {
                UserId = user.Id.ToString()
            };
        }
    }
}
