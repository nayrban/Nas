//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NASEFLibrary.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleDetailMember> RoleDetailMember { get; set; }
    }
}
