namespace GreatProj.Models
{
    public class UserDTO
    {
        
        public string Mail { get; set; }
        public string Number { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
