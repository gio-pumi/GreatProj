using GreatProj.Core.Interfaces;
using GreatProj.Core.Repositoy;
using GreatProj.Domain.Entities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GreatProj.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<User> CheckUserExistForClient(Client client)
        {
            if (client.User == null)
            {
                throw new Exception();
            }
            else if (_db.Users.Any(c => c.PersonalNumber == client.User.PersonalNumber && c.Id == client.User.Id))
            {
                if (_db.Clients.Any(c => c.User.PersonalNumber == client.User.PersonalNumber))
                {
                    throw new Exception("Client with same user Already Exist");
                }
                else
                {
                    var user = await _db.Users.FirstOrDefaultAsync(c => c.PersonalNumber == client.User.PersonalNumber && c.Id == client.User.Id);
                    return user;
                }
            }
            else
            {
                if(_db.Users.Any(c => c.Id == client.User.Id || c.PersonalNumber == client.User.PersonalNumber))
                {
                    throw new Exception("User with this  Id or Personal Number already exist");
                }
                else
                {
                    client.User.Id = 0;
                    var clientUser = client.User;
                    return clientUser;
                }
            }
        }

        public async Task<User> CheckUserExistForEmployee(Employee employee)
        {
            if (employee.User == null)
            {
                throw new Exception();
            }
            else if (_db.Users.Any(c => c.PersonalNumber == employee.User.PersonalNumber && c.Id == employee.User.Id))
            {
                if (_db.Employees.Any(c => c.User.PersonalNumber == employee.User.PersonalNumber))
                {
                    throw new Exception("Employee Already Exist");
                }
                else
                {
                    var user = await _db.Users.FirstOrDefaultAsync(c => c.PersonalNumber == employee.User.PersonalNumber && c.Id == employee.User.Id);
                    return user;
                }
            }
            else
            {
                if (_db.Users.Any(c => c.Id == employee.User.Id || c.PersonalNumber == employee.User.PersonalNumber))
                {
                    throw new Exception("User with this  Id or Personal Number already exist");
                }
                else
                {
                    employee.User.Id = 0;
                    var clientUser = employee.User;
                    return clientUser;
                }
            }
        }
    }
}
