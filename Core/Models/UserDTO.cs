using System.ComponentModel.DataAnnotations;

namespace GreatProj.Models
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MaxLength(9)]
        public string Number { get; set; }
        [Required]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
