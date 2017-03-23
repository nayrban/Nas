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
    
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            this.Device = new HashSet<Device>();
            this.MinistryMember = new HashSet<MinistryMember>();
            this.RoleDetailMember = new HashSet<RoleDetailMember>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Device { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinistryMember> MinistryMember { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleDetailMember> RoleDetailMember { get; set; }
    }
}