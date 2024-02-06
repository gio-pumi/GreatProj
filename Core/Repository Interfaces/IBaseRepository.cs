using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Models.Country;
using GreatProj.Core.Models.Employee;

namespace GreatProj.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> AddAsync(T item);
        Task<List<ClientDto>> GetAllClientAsync(GetAllClientInput input);
        Task<List<EmployeeDTO>> GetAllEmployeeAsync(GetAllEmployeeInput input);
        Task<List<CountryDto>> GetAllCountryAsync(GetAllCountryInput input);
        Task<T> GetByIdAsync(long id);
        Task<List<T>> DeleteAsync(long id);
        Task<List<T>> UpdateAsync(T item);
    }
}
