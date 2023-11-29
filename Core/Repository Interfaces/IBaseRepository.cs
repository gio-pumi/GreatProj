namespace GreatProj.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {

        Task<List<T>> AddAsync(T item);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<List<T>> DeleteAsync(long id);
        Task<List<T>> UpdateAsync(T item);

    }
}
