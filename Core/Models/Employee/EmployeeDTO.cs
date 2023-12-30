using GreatProj.Models;

namespace GreatProj.Core.Models.Employee
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public UserDTO User { get; set; }
        public string Role { get; set; }
    }
}
