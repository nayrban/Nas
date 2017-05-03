using Microsoft.AspNet.Identity.EntityFramework;
using NasModel.AuthModel;
using NasData.Configuration;
using System.Data.Entity;

namespace NasData
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base(/*"AuthContext"*/"MysqlAuthContext")
        {
            //Database.SetInitializer<AuthContext>(new AuthModelInitializer());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuthContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<AuthContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());

            //modelBuilder.Entity<ApplicationUser>()
            // .ToTable("aspnetusers")
            // .Property(us => us.Address);

            modelBuilder.Entity<IdentityUserLogin>().HasKey(ul => ul.UserId);
            modelBuilder.Entity<IdentityUserRole>().HasKey(ul => ul.UserId);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}