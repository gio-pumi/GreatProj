using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GreatProj.Core.Models.User
{
    public class UserRegiterDTO
    {
        [Required]
        [MinLength(9)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MaxLength(9)]
        public string Number { get; set; }
        [Required]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }
    }
}
