using NasData.Infraestructure;
using NasModel.AuthModel;
using NasUtilities.Utils;
using System;
using System.Linq;

namespace NasData.Respository.Auth
{
    public class AuthClientRepository : RepositoryBase<AuthContext, Client>, IAuthClientRepository
    {
        public AuthClientRepository(IDbFactory<AuthContext> dbFactory) : base(dbFactory)
        {
        }

        public Client CreateClient(Client client)
        {
            client = ComposeClient(client);
            base.Add(client);
            return client;     
        }

        public bool ClientExist(string clientName)
        {
            IQueryable<Client> result = this.DbContext.Clients.Where(client => client.Name == clientName);
            return result.Count() > 0;
        }

        private Client ComposeClient(Client client)
        {
            client.Secret = Helper.GetSecretKey();
            client.Id = Guid.NewGuid().ToString().Replace("-", "");
            client.Active = true;
            if (client.RefreshTokenLifeTime == 0) client.RefreshTokenLifeTime = 14400;
            if (string.IsNullOrEmpty(client.AllowedOrigin)) client.AllowedOrigin = "*";

            return client;
        }        
    }

    public interface IAuthClientRepository : IRepository<Client>
    {
        bool ClientExist(string clientName);

        Client CreateClient(Client client);
    }
}
