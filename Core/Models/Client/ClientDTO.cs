using GreatProj.Core.Models.Country;
using GreatProj.Models;

namespace GreatProj.Core.Models.ClientDTO
{
    public class ClientDTO
    {
        public long Id { get; set; }
        public UserDTO User { get; set; }
        public string RoomNumber { get; set; }
        public decimal Balance { get; set; }
        public long CountryId  { get; set; }
        public CountryDTO Country { get; set; }
    }
}
