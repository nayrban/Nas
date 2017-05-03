using NasData.Infraestructure;
using NasModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Respository
{
    public class MemberRepository : RepositoryBase<NasModelContext, Member>, IMemberRepository
    {
        public MemberRepository(IDbFactory<NasModelContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<Member> GetAll(int offset, int limit)
        {
             var members = DbContext.Members.
               OrderBy(m => m.MemberId).
               Skip(offset).
               Take(limit);

            return members.ToList();
        }
    }

    public interface IMemberRepository : IRepository<Member>
    {
        List<Member> GetAll(int offset, int limit);
    }
}
