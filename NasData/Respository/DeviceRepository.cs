using NasData.Infraestructure;
using NasModel.Model;

namespace NasData.Respository
{
    public interface IDeviceRepository : IRepository<Device>
    {

    }
    public class DeviceRepository : RepositoryBase<NasModelContext, Device>, IDeviceRepository
    {
        public DeviceRepository(IDbFactory<NasModelContext> dbFactory) : base(dbFactory)
        {
        }
    }
}
