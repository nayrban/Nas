using NasData.Infraestructure;
using NasModel.Model;
using NasUtilities.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Respository
{
    public interface IMinistryCodeRepository : IRepository<MinistryCode>
    {
        MinistryCode Create(int ministryId);

        MinistryCode GetByCode(string code);
    }

    public class MinistryCodeRepository : RepositoryBase<NasModelContext, MinistryCode>, IMinistryCodeRepository
    {
        public MinistryCodeRepository(IDbFactory<NasModelContext> dbFactory) : base(dbFactory)
        {
        }

        public MinistryCode Create(int ministryId)
        {
            MinistryCode ministryCode = ComposeMinistryCode(ministryId);
            base.Add(ministryCode);
            return ministryCode;
        }

        public MinistryCode GetByCode(string code)
        {
            return DbContext.MinistryCodes.Where(r => r.Code == code).SingleOrDefault();
        }

        private MinistryCode ComposeMinistryCode(int ministryId)
        {
            return new MinistryCode() { Code = Helper.RandomString(6), Used = false, MinistryId = ministryId };
        }
    }
}
