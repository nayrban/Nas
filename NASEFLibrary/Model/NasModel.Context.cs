﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NasEntities : DbContext
    {
        public NasEntities()
            : base("name=NasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
           // throw new UnintentionalCodeFirstException();

        }
    
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Ministry> Ministries { get; set; }
        public virtual DbSet<MinistryMember> MinistryMembers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleDetail> RoleDetails { get; set; }
        public virtual DbSet<RoleDetailMember> RoleDetailMembers { get; set; }
        public virtual DbSet<RoleMinistry> RoleMinistries { get; set; }
    }
}
