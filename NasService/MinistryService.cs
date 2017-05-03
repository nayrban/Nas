using NasData;
using NasData.Infraestructure;
using NasData.Respository;
using NasModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasService
{
    public interface IMinistryService : IDisposable
    {
        MinistryCode CreateMinistryCode(int ministryId);

        MinistryCode GetMinistryCodeByCode(string code);

        IEnumerable<Ministry> GetAll();

        void SaveChanges();

        Task<bool> SaveChangesAsync();

    }

    public class MinistryService : Disposable, IMinistryService
    {
        private readonly IMinistryCodeRepository ministryCodeRepository;
        private readonly IMinistyRepository ministryRepository;
        private readonly IUnitOfWork<NasModelContext> unityOfWork;

        public MinistryService(IMinistyRepository ministryRepository, IMinistryCodeRepository ministryCodeRepository, IUnitOfWork<NasModelContext> unityOfWork)
        {
            this.ministryRepository = ministryRepository;
            this.ministryCodeRepository = ministryCodeRepository;
            this.unityOfWork = unityOfWork;
        }

        public MinistryCode CreateMinistryCode(int ministryId)
        {
            return ministryCodeRepository.Create(ministryId);
        }

        public IEnumerable<Ministry> GetAll()
        {
            return ministryRepository.GetAll();
        }

        public MinistryCode GetMinistryCodeByCode(string code)
        {
            return ministryCodeRepository.GetByCode(code);
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
