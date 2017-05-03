using NasModel.AuthModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "MinistryMember")]
    public class MinistryMember
    {
        public int id { get; set; }
        public Nullable<int> ministryId { get; set; }
        public Nullable<int> userId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Ministry Ministry { get; set; }
    }
}