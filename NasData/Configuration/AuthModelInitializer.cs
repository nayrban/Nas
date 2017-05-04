using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NasModel.AuthModel;
using System.Data.Entity;

namespace NasData.Configuration
{
    public class AuthModelInitializer : CreateDatabaseIfNotExists<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {            
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string [] roles = new string[] { "Admin", "App", "Web" };
                       
            IdentityResult roleResult;

            foreach(string  roleName in roles) {             
                if (!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));
                }
            }

            var user = UserManager.FindByName("admin");
            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Name = "Admin",
                    LastName = "User",
                    Email = "nayrban@gmail.com",
                    PhoneNumber = "+50688062177",                    
                };
                UserManager.Create(newUser, "1Pa55word2");
                UserManager.SetLockoutEnabled(newUser.Id, false);
                UserManager.AddToRole(newUser.Id, "Admin");
                base.Seed(context);
            }
        }
    }
}
