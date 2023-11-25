using GreatProj.Domain.Entities;
using GreatProj.Models;

namespace GreatProj.Core.Models.ClientDTO
{
    public class ClientDTO
    {

        public UserDTO User { get; set; }
        public string RoomNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
