using System.ComponentModel.DataAnnotations;

namespace NasDTOUtils.Dto.Request
{
    public class MinistryCodeRequest
    {
        [Required]
        public int MinistryId { get; set; }
    }
}