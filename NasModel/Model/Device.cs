using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.Model
{
    [Table(name:"Device")]
    public class Device
    {
        public int Id { get; set; }
        public string NotificationKey { get; set; }
        public string OsVersion { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}
