using Tdf.Act.Domain.Repositories;
using Tdf.Act.Domain.Services;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class ChangePwdCommandExecutor : ICommandExecutor<ChangePwdCommand>
    {
        public IUserRepository _userRepository;

        public ChangePwdCommandExecutor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(ChangePwdCommand cmd)
        {
            var service = new UserService(_userRepository);
            var user = service.ChangePwd(cmd.UserId, cmd.OldPwd, cmd.NewPwd);

            cmd.ExecutionResult = new ChangePwdCommandResult
            {
                UserId = user.Id.ToString()
            };
        }
    }
}
