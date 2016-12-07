using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;
using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class SaveTokenCommandExecutor : ICommandExecutor<SaveTokenCommand>
    {
        public IRefreshTokenRepository _refreshTokenRepository;

        public SaveTokenCommandExecutor(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public void Execute(SaveTokenCommand cmd)
        {
            var entity = new RefreshToken();
            entity.Id = cmd.RefreshTokenId;
            entity.UserId = cmd.UserId;
            entity.IssuedUtc = cmd.IssuedUtc;
            entity.ExpiresUtc = cmd.ExpiresUtc;
            entity.ProtectedTicket = cmd.ProtectedTicket;

            _refreshTokenRepository.SaveToken(entity);
        }
    }
}
