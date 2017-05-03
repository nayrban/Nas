using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "Ministry")]
    public class Ministry
    {        
        public Ministry()
        {
            this.MinistryMember = new HashSet<MinistryMember>();
            this.RoleMinistry = new HashSet<RoleMinistry>();
            this.MinistryCode = new HashSet<MinistryCode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<MinistryMember> MinistryMember { get; set; }        
        public virtual ICollection<RoleMinistry> RoleMinistry { get; set; }       
        public virtual ICollection<MinistryCode> MinistryCode { get; set; }
    }
}