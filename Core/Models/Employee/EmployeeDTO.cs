using GreatProj.Core.Models.User;

namespace GreatProj.Core.Models.Employee
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public UserDto User { get; set; }
        public string Role { get; set; }
    }
}
