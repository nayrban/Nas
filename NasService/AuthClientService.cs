using System;
using NasData;
using NasData.Infraestructure;
using NasData.Respository;
using NasModel.AuthModel;
using System.Threading.Tasks;
using NasData.Respository.Auth;

namespace NasService
{
    public interface IAuthClientService : IDisposable
    {
        Client FindClient(string clientId);

        Client CreateClient(Client client);

        bool ClientExist(string clientName);

        void SaveChanges();

        Task<bool> SaveChangesAsync();

    }
    public class AuthClientService : Disposable, IAuthClientService
    {
        private readonly IAuthClientRepository clientRepository;
        private readonly IUnitOfWork<AuthContext>  unityOfWork;

        public AuthClientService(IAuthClientRepository clientRepository, IUnitOfWork<AuthContext> unityOfWork)
        {
            this.clientRepository = clientRepository;
            this.unityOfWork = unityOfWork;
        }

        public bool ClientExist(string clientName)
        {
            return clientRepository.ClientExist(clientName);
        }

        public Client CreateClient(Client client)
        {
           return clientRepository.CreateClient(client);
        }

        public Client FindClient(string clientId)
        {
            return clientRepository.GetById(clientId);
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
