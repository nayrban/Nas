using NASEFLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasScheduleService.Dto
{
    public class MinistryNotification
    {
        public string MinistryName { get; set; }   
        public string MinistryDescription { get; set; }
        public ICollection<MinistryMember> MinistryMembers { get; set; }      
        public string RoleName { set; get; }
        public string RoleDescription { set; get; }
        public string RoleDetailNotKeys { get; set; }
        public string RoleDetailDescription { get; set; }
        public string RoleDetailName { get; set; }       
        public DateTime? RoleDetailSchedule { set; get; }
        public ICollection<RoleDetailMember> RoleDetailMembers { get; set; }        
    }
}
