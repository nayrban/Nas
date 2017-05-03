using NasModel.AuthModel;
using System.Data.Entity.ModelConfiguration;

namespace NasData.Configuration
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            ToTable("Client");            
            Property(c => c.Name).IsRequired().HasMaxLength(100);            
            Property(c => c.AllowedOrigin).HasMaxLength(100);
        }
    }
}
