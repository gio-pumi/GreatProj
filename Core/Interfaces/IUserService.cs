using GreatProj.Domain.Entities;

namespace GreatProj.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> CheckUserExistForClient(Client client);
        Task<User> CheckUserExistForEmployee(Employee employee);
    }
}
