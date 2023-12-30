using GreatProj.Models;

namespace GreatProj.Core.Models.Client
{
    public class ClientAddDTO
    {
        public long Id { get; set; }
        public UserDTO User { get; set; }
        public string RoomNumber { get; set; }
        public decimal Balance { get; set; }
        public long CountryId { get; set; }
    }
}
