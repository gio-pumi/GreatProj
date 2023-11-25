using GreatProj.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatProj.Core.Models.ClientDTO
{
    public class ClientUpdateDTO
    {
        public long Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
