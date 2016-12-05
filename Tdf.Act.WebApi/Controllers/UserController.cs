﻿using System;
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
        public ServiceResult Login([FromUri]LoginCommand command)
        {
            _commandBus.Send(command);
            var currentUserId = command.ExecutionResult.UserId;
            return new ServiceResult(currentUserId);
        }

        [Route("api/ActUser/ChangePwd")]
        [HttpGet]
        public void ChangePwd(ChangePwdCommand command)
        {
            _commandBus.Send(command);
        }
    }
}
