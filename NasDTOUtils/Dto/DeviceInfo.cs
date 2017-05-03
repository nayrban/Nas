using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasDTOUtils.Dto
{
    public class DeviceInfo
    {
        public int Id { get; set; }
        [Required]
        public string NotificationKey { get; set; }
        [Required]
        public string OsVersion { get; set; }
    }
}