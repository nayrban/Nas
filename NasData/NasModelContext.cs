using Microsoft.AspNet.Identity.EntityFramework;
using NasModel.Model;
using System.Data.Entity;

namespace NasData
{
    public class NasModelContext : DbContext
    {

        public NasModelContext()
            : base("NasModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Device> Devices { get; set; }        
        public virtual DbSet<Ministry> Ministries { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MinistryMember> MinistryMembers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleDetail> RoleDetails { get; set; }
        public virtual DbSet<RoleDetailMember> RoleDetailMembers { get; set; }
        public virtual DbSet<RoleMinistry> RoleMinistries { get; set; }
        public virtual DbSet<MinistryCode> MinistryCodes { get; set; }
    }
}
