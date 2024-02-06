using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GreatProj.Core.Models.User
{
    public class UserDto
    {
        public long Id { get; set; }
        [Required]
        [MinLength(9)]
        public string UserName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
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
