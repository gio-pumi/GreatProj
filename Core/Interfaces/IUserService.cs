using GreatProj.Domain.Entities;

namespace GreatProj.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<Client>> AddClient(Client client);
        Task<List<Employee>> AddEmployee(Employee employee);
    }
}
