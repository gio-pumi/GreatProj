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
        private readonly IClientRepository<Client> _clientRepository;
        private readonly IEmployeeRepository<Employee> _employeeRepository;

        public UserService(
            IClientRepository<Client> clientRepository, 
            IEmployeeRepository<Employee> employeeRepository ,
            AppDbContext context)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _db = context;
        }

        public async Task<List<Client>> AddClient(Client client)
        {
            if (await _db.Clients.Include(c => c.User).AnyAsync(c => c.User.PersonalNumber == client.User.PersonalNumber))
            {
                throw new Exception("Client With Same User Already Exist");
            }
            var user = await _db.Users.FirstOrDefaultAsync(c => c.PersonalNumber == client.User.PersonalNumber);
            if (user != null)
            {
                client.UserId = user.Id;
                client.User = null;
            }
            var clients = await _clientRepository.AddAsync(client);
            return clients;
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
