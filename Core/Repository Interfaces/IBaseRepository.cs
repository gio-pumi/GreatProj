using GreatProj.Core.Models.ClientDTO;
using System.Linq.Expressions;

namespace GreatProj.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {

        Task<List<T>> AddAsync(T item);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<List<T>> DeleteAsync(long id);
        Task<List<T>> UpdateAsync(T item);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

    }
}
