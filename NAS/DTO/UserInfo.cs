using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAS.DTO
{
    public class UserInfo
    {        
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string lastName { get; set; }
        [Required]
        public string phone { get; set; }
        public string address { get; set; }
        [Required]
        public string email { get; set; }
        public Nullable<int> age { get; set; }
        [Required]
        public Nullable<System.DateTime> dob { get; set; }
    }
}