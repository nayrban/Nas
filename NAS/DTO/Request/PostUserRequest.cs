using NASEFLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAS.DTO.Request
{
    public class PostUserRequest 
    {
        public UserInfo UserInfo { get; set; }
        public DeviceInfo DeviceInfo { get; set; }
      
    }
}