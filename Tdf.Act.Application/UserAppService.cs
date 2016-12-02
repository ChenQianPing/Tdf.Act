using Tdf.Act.Domain.Commands;
using Tdf.Commanding;

namespace Tdf.Act.Application
{
    public class UserAppService
    {
        public ICommandBus _commandBus { get; private set; }

        public UserAppService(ICommandBus commandBus)
        {
            this._commandBus = commandBus;
        }


        public void Register(RegisterCommand command)
        {
            _commandBus.Send(command);
        }

        public void Login(LoginCommand command)
        {
            _commandBus.Send(command);
        }

        public void ChangePwd(ChangePwdCommand command)
        {
            _commandBus.Send(command);
        }

    }

    
}
