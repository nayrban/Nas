using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name:"Member")]
    public class Member
    {
        public Member()
        {
            this.Device = new HashSet<Device>();
            this.MinistryMember = new HashSet<MinistryMember>();
            this.RoleDetailMember = new HashSet<RoleDetailMember>();
        }
        
        public int MemberId { set; get; }
        public string UserId { set; get; }

        public bool IsMember { get; set; }

        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<MinistryMember> MinistryMember { get; set; }
        public virtual ICollection<RoleDetailMember> RoleDetailMember { get; set; }
    }
}
