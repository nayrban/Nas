using NasData;
using NasData.Infraestructure;
using NasData.Respository;
using NasModel.Model;
using System;
using System.Threading.Tasks;

namespace NasService
{
    public interface IDeviceService : IDisposable
    {
        void CreateDevice(Device device);        

        void SaveChanges();

        Task<bool> SaveChangesAsync();

    }

    public class DeviceService : Disposable, IDeviceService
    {
        private readonly IDeviceRepository deviceRepository;        
        private readonly IUnitOfWork<NasModelContext> unityOfWork;

        public DeviceService(IDeviceRepository deviceRepository, IUnitOfWork<NasModelContext> unityOfWork)
        {
            this.deviceRepository = deviceRepository;            
            this.unityOfWork = unityOfWork;
        }

        public void CreateDevice(Device device)
        {            
            deviceRepository.Add(device);
        }

        public void SaveChanges()
        {
            unityOfWork.Commit();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await unityOfWork.CommitAsync();
        }
    }
}
