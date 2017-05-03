using System;
using System.ComponentModel.DataAnnotations;

namespace NasDTOUtils.Dto
{
    public class UserInfo
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int MemberId { get; set; }        
        public string Name { get; set; }
        public string LastName { get; set; }        
        public string PhoneNumber { get; set; }
        public string Address { get; set; }        
        public string Email { get; set; }
        public int? Age { get; set; }        
        public DateTime? Dob { get; set; }
    }
}