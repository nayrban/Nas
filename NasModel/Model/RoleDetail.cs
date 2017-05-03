using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "RoleDetail")]
    public class RoleDetail
    {
        public RoleDetail()
        {
            this.RoleDetailMember = new HashSet<RoleDetailMember>();
        }

        public int id { get; set; }
        public int roleId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> schedule { get; set; }
        public string notificationsKeyDays { get; set; }

        public virtual Role Role { get; set; }        
        public virtual ICollection<RoleDetailMember> RoleDetailMember { get; set; }
    }
}