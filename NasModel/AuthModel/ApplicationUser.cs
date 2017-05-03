using Microsoft.AspNet.Identity.EntityFramework;
using NasModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasModel.AuthModel
{
    [Table(name: "aspnetusers")]
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {            
        }

        public string Name { get; set; }        
        public string LastName { get; set; }
        public string Address { get; set; }              
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }  
    }
}
