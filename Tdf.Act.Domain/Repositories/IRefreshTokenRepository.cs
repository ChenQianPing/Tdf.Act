using System.Threading.Tasks;
using Tdf.Act.Domain.Entities;
using Tdf.Domain.Repositories;

namespace Tdf.Act.Domain.Repositories
{
    public interface IRefreshTokenRepository :IRepository<RefreshToken>
    {
        RefreshToken GetToken(string tokenId);
        void SaveToken(RefreshToken refreshToken);
        void RemoveToken(string tokenId);
    }
}
