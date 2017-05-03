using NasModel.AuthModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Configuration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            ToTable("AspNetUsers");
            Property(c => c.Name).HasMaxLength(100);
            Property(c => c.Address).HasColumnType("NVARCHAR").HasMaxLength(100);
        }
    }
}
