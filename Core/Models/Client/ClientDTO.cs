using GreatProj.Core.Models.Country;
using GreatProj.Core.Models.User;

namespace GreatProj.Core.Models.ClientDto
{
    public class ClientDto
    {
        public long Id { get; set; }
        public UserDto User { get; set; }
        public string RoomNumber { get; set; }
        public decimal Balance { get; set; }
        public long CountryId { get; set; }
        public CountryDto Country { get; set; }
        public string Language { get; set; }
    }
}
