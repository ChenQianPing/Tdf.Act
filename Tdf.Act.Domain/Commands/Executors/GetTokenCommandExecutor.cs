using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Act.Domain.Repositories;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class GetTokenCommandExecutor : ICommandExecutor<GetTokenCommand>
    {
        public IRefreshTokenRepository _refreshTokenRepository;

        public GetTokenCommandExecutor(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public void Execute(GetTokenCommand cmd)
        {
            var token = _refreshTokenRepository.GetToken(cmd.TokenId);

            cmd.ExecutionResult = new GetTokenCommandResult
            {
                RefreshTokenId = token.Id,
                UserId = token.UserId,
                IssuedUtc = token.IssuedUtc,
                ExpiresUtc = token.ExpiresUtc,
                ProtectedTicket = token.ProtectedTicket
            };
        }
    }
}
