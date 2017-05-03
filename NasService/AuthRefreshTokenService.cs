using System;
using NasData;
using NasData.Infraestructure;
using NasModel.AuthModel;
using System.Threading.Tasks;
using NasData.Respository.Auth;

namespace NasService
{
    public interface IAuthRefreshTokenService : IDisposable
    {
        Task<bool> AddRefreshToken(RefreshToken token);

        Task<bool> RemoveRefreshToken(string refreshTokenId);

        Task<RefreshToken> FindRefreshToken(string refreshTokenId);

        void SaveChanges();

    }
    public class AuthRefreshTokenService : Disposable, IAuthRefreshTokenService
    {
        private readonly IAuthRefreshTokenRepository refreshTokenRepository;
        private readonly IUnitOfWork<AuthContext>  unityOfWork;

        public AuthRefreshTokenService(IAuthRefreshTokenRepository refreshTokenRepository, IUnitOfWork<AuthContext> unityOfWork)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.unityOfWork = unityOfWork;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            return await refreshTokenRepository.AddRefreshToken(token);
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return await refreshTokenRepository.FindRefreshToken(refreshTokenId);
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return await refreshTokenRepository.RemoveRefreshToken(refreshTokenId);
        }

        public void SaveChanges()
        {
            unityOfWork.Commit();
        }
    }
}
