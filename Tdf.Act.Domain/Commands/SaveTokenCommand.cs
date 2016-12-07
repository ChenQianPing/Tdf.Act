using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands
{
    public class SaveTokenCommand : ICommand
    {
        public Guid RefreshTokenId { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }

        public SaveTokenCommand() { }
        
    }

}
