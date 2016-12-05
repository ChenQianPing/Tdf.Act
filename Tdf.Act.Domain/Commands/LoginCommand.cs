﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands
{
    public class LoginCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginCommandResult ExecutionResult { get; set; }

        public LoginCommand() {}
    }

    public class LoginCommandResult
    {
        public string UserId { get; set; }
    }
}
