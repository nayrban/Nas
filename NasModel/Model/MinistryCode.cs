using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "MinistryCode")]
    public class MinistryCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
        
        public Nullable<int> MembeId { get; set; }
        
        public int MinistryId { get; set; }

        public virtual Ministry Ministry { get; set; }
    }
}