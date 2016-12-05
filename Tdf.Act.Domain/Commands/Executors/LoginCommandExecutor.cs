using Tdf.Act.Domain.Repositories;
using Tdf.Act.Domain.Services;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class LoginCommandExecutor : ICommandExecutor<LoginCommand>
    {
        public IUserRepository _repository;

        public LoginCommandExecutor(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(LoginCommand cmd)
        {
            var service = new UserService(_repository);
            var user = service.Login(cmd.UserName, cmd.Password);

            cmd.ExecutionResult = new LoginCommandResult
            {
                UserId = user.Id.ToString()
            };
        }
    }
}
