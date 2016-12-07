using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands
{
    public class GetTokenCommand : ICommand
    {
        public string TokenId { get; set; }

        public GetTokenCommandResult ExecutionResult { get; set; }
    }

    public class GetTokenCommandResult
    {
        public Guid RefreshTokenId { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
