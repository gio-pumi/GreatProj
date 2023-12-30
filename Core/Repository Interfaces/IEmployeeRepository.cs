using GreatProj.Core.Interfaces;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Repository_Interfaces
{
    public interface IEmployeeRepository<T> : IBaseRepository<Employee> 
    {
    }
}
