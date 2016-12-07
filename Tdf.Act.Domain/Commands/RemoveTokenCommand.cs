using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands
{
    public class RemoveTokenCommand : ICommand
    {
        public string TokenId { get; set; }
    }
}
