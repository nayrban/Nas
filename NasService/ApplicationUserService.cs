using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NasData;
using NasData.Infraestructure;
using NasData.Respository;
using NasData.Respository.Auth;
using NasModel.AuthModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NasService
{


    public interface IApplicationUserService : IDisposable
    {
        Task<IdentityResult> RegisterUser(ApplicationUser userModel, string password);

        Task<IdentityUser> FindUser(string userName, string password);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        Task<ApplicationUser> FindUser(string userName);

        Task<IdentityResult> UpdateUser(ApplicationUser userModel);

        IQueryable<ApplicationUser> GetAll(int offset, int limit);        

        void SaveChanges();

        Task<bool> SaveChangesAsync();
        int Count();


    }
    public class ApplicationUserService : Disposable, IApplicationUserService
    {
        private readonly IApplicationUserRepository userRepository;
        private readonly IDeviceRepository deviceRepository;
        private readonly IUnitOfWork<NasModelContext> unityOfWork;

        public ApplicationUserService(IApplicationUserRepository userRepository, IDeviceRepository deviceRepository, IUnitOfWork<NasModelContext> unityOfWork)
        {
            this.userRepository = userRepository;
            this.unityOfWork = unityOfWork;
            this.deviceRepository = deviceRepository;
        }    

        public async Task<IdentityResult> RegisterUser(ApplicationUser userModel, string password)
        {
            return await userRepository.RegisterUser(userModel, password);
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            return await userRepository.FindUser(userName, password);
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            return await userRepository.AddLoginAsync(userId, login);            
        }

        public async Task<ApplicationUser> FindUser(string userName)
        {
            return await userRepository.FindUser(userName);
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser userModel)
        {
            return await userRepository.UpdateUser(userModel);
        }

        public IQueryable<ApplicationUser> GetAll(int offset, int limit)
        {
            return userRepository.GetAll(offset, limit);
        }


        public void SaveChanges()
        {
            unityOfWork.Commit();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await unityOfWork.CommitAsync();
        }

        public int Count()
        {
            return userRepository.Count();
        }
    }
}
