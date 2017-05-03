using NasData.Infraestructure;
using NasModel.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Respository.Auth
{
    public class AuthRefreshTokenRepository :  RepositoryBase<AuthContext, RefreshToken>, IAuthRefreshTokenRepository
    {
        public AuthRefreshTokenRepository(IDbFactory<AuthContext> dbFactory) : base(dbFactory)
        {
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = this.DbContext.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                base.Delete(existingToken);
            }

            base.Add(token);

            return await this.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await this.DbContext.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                base.Delete(refreshToken);
                return await this.DbContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await this.DbContext.RefreshTokens.FindAsync(refreshTokenId);
           
            return refreshToken;
        }
    }

    public interface IAuthRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<bool> AddRefreshToken(RefreshToken token);

        Task<bool> RemoveRefreshToken(string refreshTokenId);

        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
    }
}
