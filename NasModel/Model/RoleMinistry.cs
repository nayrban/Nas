using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name: "RoleMinistry")]
    public class RoleMinistry
    {
        public int Id { get; set; }
        public int MinistryId { get; set; }
        public int RoleId { get; set; }

        public virtual Ministry Ministry { get; set; }
        public virtual Role Role { get; set; }
    }
}