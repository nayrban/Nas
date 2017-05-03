using NasData.Infraestructure;
using NasModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Respository
{
    public class MinistryRepository : RepositoryBase<NasModelContext, Ministry>, IMinistyRepository
    {
        public MinistryRepository(IDbFactory<NasModelContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<Ministry> GetAll(int offset, int limit)
        {
             var ministries = DbContext.Ministries.
               OrderBy(m => m.Id).
               Skip(offset).
               Take(limit);

            return ministries.ToList();
        }
    }

    public interface IMinistyRepository : IRepository<Ministry>
    {
        List<Ministry> GetAll(int offset, int limit);
    }
}
