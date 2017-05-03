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
    public interface IMemberService : IDisposable
    {
        Member Cretate(string userId);       

        void SaveChanges();

        Task<bool> SaveChangesAsync();

    }

    public class MemberService : Disposable, IMemberService
    {
        private readonly IMemberRepository memberRepository;        
        private readonly IUnitOfWork<NasModelContext> unityOfWork;

        public MemberService(IMemberRepository memberRepository, IUnitOfWork<NasModelContext> unityOfWork)
        {
            this.memberRepository = memberRepository;            
            this.unityOfWork = unityOfWork;
        }

        public Member Cretate(string userId)
        {
            Member member = new Member() { UserId = userId, IsMember = false };

            memberRepository.Add(member);
            return member;
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
