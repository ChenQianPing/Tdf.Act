using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands
{
    public class ChangePwdCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string OldPwd { get; set; }
        public string NewPwd { get; set; }

        public ChangePwdCommandResult ExecutionResult { get; set; }
        public ChangePwdCommand()
        { }
    }

    public class ChangePwdCommandResult
    {
        public string UserId { get; set; }
    }
}
