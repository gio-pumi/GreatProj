using GreatProj.Domain.Entities;
using GreatProj.Models;

namespace GreatProj.Core.Models.Employee
{
    public class EmployeeDTO
    {
        public UserDTO User { get; set; }
        public string Role { get; set; }
    }
}
