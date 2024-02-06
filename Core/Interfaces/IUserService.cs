using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<Employee>> AddEmployee(Employee employee);
    }
}
