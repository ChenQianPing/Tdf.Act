using System;
using System.Linq;
using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;

namespace Tdf.Act.EntityFramework.Repositories
{
    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private ActDbContext _dbContext;

        public RefreshTokenRepository(ActDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public RefreshToken GetToken(string tokenId)
        {
            var tokenGuid = new Guid(tokenId);
            var entity = _dbContext.Set<RefreshToken>().FirstOrDefault(p => p.Id == tokenGuid);

            return entity;
        }

        public void RemoveToken(string tokenId)
        {
            var tokenGuid = new Guid(tokenId);
            var entity = _dbContext.RefreshTokens.FirstOrDefault(b => b.Id == tokenGuid);
            _dbContext.RefreshTokens.Remove(entity);
        }

        public void SaveToken(RefreshToken refreshToken)
        {
            foreach (RefreshToken entity in _dbContext.RefreshTokens.Where(b => b.UserId == refreshToken.UserId).ToList())
            {
                _dbContext.RefreshTokens.Remove(entity);
            }

            _dbContext.RefreshTokens.Add(refreshToken);
        }
    }
}
