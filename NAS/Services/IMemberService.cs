using NAS.DTO;
using NAS.DTO.Request;
using NASEFLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAS.Services
{
    public interface IMemberService
    {
        UserInfo Create(PostUserRequest userRequest);

        UserInfo GetMemberByID(int id);

        List<UserInfo> GetAll(int offset, int limit);

        int Count();
    }
}
