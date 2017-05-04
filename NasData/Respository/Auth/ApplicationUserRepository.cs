using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NasData.Infraestructure;
using NasModel.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasData.Respository.Auth
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<IdentityResult> RegisterUser(ApplicationUser userModel, string password, List<string> roles);

        Task<IdentityResult> UpdateUser(ApplicationUser userModel);

        Task<ApplicationUser> FindUser(string userName, string password);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        Task<IList<string>> UserRoles(string userId);

        Task<ApplicationUser> FindUser(string userName);

        IQueryable<ApplicationUser> GetAll(int offset, int limit);
        int Count();

    }

    public class ApplicationUserRepository : RepositoryBase<AuthContext, ApplicationUser>, IApplicationUserRepository
    {        

        private UserManager<ApplicationUser> _userManager;        

        public ApplicationUserRepository(IDbFactory<AuthContext> dbFactory) : base(dbFactory)
        {            
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));            
        }

        public async Task<IdentityResult> RegisterUser(ApplicationUser user, string password, List<string> roles)
        {            

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                foreach(string role in roles) { 
                    await _userManager.AddToRoleAsync(user.Id, role);
                }
            }

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        
        public void Dispose()
        {            
            _userManager.Dispose();

        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {            
            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public int Count()
        {
            return _userManager.Users.Count();
        }

        public IQueryable<ApplicationUser> GetAll(int offset, int limit)
        {
            var users = _userManager.Users.OrderBy(m => m.Id).
            Skip(offset).
            Take(limit);

            return users;
        }

        public async Task<IList<string>> UserRoles(string userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }
    }
}