using GreatProj.Core.Interfaces;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatProj.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IEmployeeRepository<Employee> _employeeRepository;

        public UserService(
            IEmployeeRepository<Employee> employeeRepository,
            AppDbContext context)
        {
            _employeeRepository = employeeRepository;
            _db = context;
        }
        public async Task<List<Employee>> AddEmployee(Employee employee)
        {
            if (await _db.Employees.Include(e => e.User).AnyAsync(c => c.User.PersonalNumber == employee.User.PersonalNumber))
            {
                throw new Exception("Employee With Same User Already Exist");
            }
            var user = await _db.Users.FirstOrDefaultAsync(c => c.PersonalNumber == employee.User.PersonalNumber);
            if (user != null)
            {
                employee.UserId = user.Id;
            }
            var employees = await _employeeRepository.AddAsync(employee);
            return employees;
        }
    }
}
