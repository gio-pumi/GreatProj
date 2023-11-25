using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.Entities;
using GreatProj.Infrastructure.Data;

namespace GreatProj.Core.Repositoy
{
    public class EmployeeRepository<T> : BaseRepository<Employee>, IEmployeeRepository<Employee>
    {
        public EmployeeRepository(AppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
