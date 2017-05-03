using System.ComponentModel.DataAnnotations;

namespace NasDTOUtils.Dto.Request
{
    public class PostUserRequest 
    {
        [Required]
        public UserInfo UserInfo { get; set; }
        [Required]
        public DeviceInfo DeviceInfo { get; set; }
      
    }
}