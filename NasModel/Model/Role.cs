using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "Role")]
    public class Role
    {
        public Role()
        {
            this.RoleDetail = new HashSet<RoleDetail>();
            this.RoleMinistry = new HashSet<RoleMinistry>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public virtual ICollection<RoleDetail> RoleDetail { get; set; }        
        public virtual ICollection<RoleMinistry> RoleMinistry { get; set; }
    }
}