using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.Employee;
using GreatProj.Domain.Entities;

namespace GreatProj.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> AddAsync(T item);
        Task<List<Client>> GetAllClientAsync(GetAllClientInput input);
        Task<List<Employee>> GetAllEmployeeAsync(GetAllEmployeeInput input); 
        Task<T> GetByIdAsync(long id);
        Task<List<T>> DeleteAsync(long id);
        Task<List<T>> UpdateAsync(T item);

    }
}
