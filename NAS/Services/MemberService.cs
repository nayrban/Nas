using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.DTO.Request;
using NASEFLibrary.Model;
using NAS.DTO;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace NAS.Services
{
    public class MemberService : IMemberService
    {
        NasEntities _nasEntities;
        public MemberService()
        {
            this._nasEntities = new NasEntities();
        }

        public int Count()
        {
            return this._nasEntities.Members.Count();
        }

        public UserInfo Create(PostUserRequest userRequest)
        {
            Member member = new Member {
                name = userRequest.UserInfo.name,
                lastName = userRequest.UserInfo.lastName,
                address = userRequest.UserInfo.address,
                email = userRequest.UserInfo.email,
                phone = userRequest.UserInfo.phone,
                dob = userRequest.UserInfo.dob,
                age = userRequest.UserInfo.age,
            };

            this._nasEntities.Members.Add(member);
            
            Device device = new Device
            {
                osVersion = userRequest.DeviceInfo.osVersion,
                notificationKey = userRequest.DeviceInfo.notificationKey,
                idMember = member.id,
            };

            this._nasEntities.SaveChanges();
            
            return Mapper.Map<UserInfo>(member);
        }

        public List<UserInfo> GetAll(int offset, int limit)
        {
            var members = this._nasEntities.Members.
                OrderBy(m => m.id).
                Skip(offset).
                Take(limit);

            return members.ProjectTo<UserInfo>(Mapper.Instance.ConfigurationProvider).ToList();
        }

        public UserInfo GetMemberByID(int id)
        {            
            Member member = this._nasEntities.Members.Find(id);
 
            return Mapper.Map<UserInfo>(member);
        }


    }
}