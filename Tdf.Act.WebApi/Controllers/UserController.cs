using System;
using System.Web.Http;
using Tdf.Act.Domain.Commands;
using Tdf.Commanding;
using Tdf.Dependency;
using Tdf.Utils.Networking;

namespace Tdf.Act.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public ICommandBus _commandBus { get; private set; }


        public UserController()
            : this(ObjectContainer.Resolve<ICommandBus>())
        {
        }

        public UserController(ICommandBus commandBus)
        {
            this._commandBus = commandBus;
        }

        [Route("api/ActUser/Register")]
        [HttpGet]
        public void Register(RegisterCommand command)
        {
            _commandBus.Send(command);
        }

        [Route("api/ActUser/Login")]
        [HttpGet]
        public ServiceResult Login(string userName,string password)
        {
            try
            {
                var command = new LoginCommand();
                command.UserName = userName;
                command.Password = password;

                _commandBus.Send(command);
                var currentUserId = command.ExecutionResult.UserId;
                return new ServiceResult(currentUserId);
            }
            catch (Exception ex)
            {
                return new ServiceResult(-1, ex.Message, null);
            }
            
        }

        [Route("api/ActUser/ChangePwd")]
        [HttpGet]
        public void ChangePwd(ChangePwdCommand command)
        {
            _commandBus.Send(command);
        }
    }
}
