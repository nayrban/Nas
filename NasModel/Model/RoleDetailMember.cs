using NasModel.AuthModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "RoleDetailMember")]
    public class RoleDetailMember
    {
        public int id { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> roleDetailId { get; set; }

        public virtual Member Member { get; set; }
        public virtual RoleDetail RoleDetail { get; set; }
    }
}