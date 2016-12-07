using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Act.Domain.Repositories;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class RemoveTokenCommandExecutor : ICommandExecutor<RemoveTokenCommand>
    {
        public IRefreshTokenRepository _refreshTokenRepository;

        public RemoveTokenCommandExecutor(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public void Execute(RemoveTokenCommand cmd)
        {
            _refreshTokenRepository.RemoveToken(cmd.TokenId);
        }
    }
}
