using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAS.DTO
{
    public class DeviceInfo
    {
        public int id { get; set; }
        [Required]
        public string notificationKey { get; set; }
        [Required]
        public string osVersion { get; set; }
    }
}