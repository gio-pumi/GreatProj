using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.Employee;
using GreatProj.Domain.Entities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreatProj.Core.Repositoy
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _db;

        public BaseRepository(AppDbContext DbContext)
        {
            _db = DbContext;
        }
        public virtual async Task<List<T>> AddAsync(T item)
        {
            _db.Set<T>().Add(item);
            await _db.SaveChangesAsync();
            var items = await _db.Set<T>().ToListAsync();
            return items;
        }

        public virtual async Task<List<T>> DeleteAsync(long id)
        {
            T itemToDelete = await _db.Set<T>().FindAsync(id);
            _db.Set<T>().Remove(itemToDelete);
            await _db.SaveChangesAsync();
            var items = await _db.Set<T>().ToListAsync();
            return items;
        }

        public virtual async Task<List<Client>> GetAllClientAsync(GetAllClientInput input)
        {
            //Filtering according input
            IQueryable<Client> query = _db.Set<Client>();
            query = query.Where(item =>
                          item.User.PersonalNumber.Contains(input.PersonalNumber) ||
                          item.User.Mail.Contains(input.Mail) ||
                          item.User.Number.Contains(input.Number) ||
                          item.RoomNumber.Contains(input.RoomNumber) ||
                          (item.User.CreateDate >= input.StartDate && item.User.CreateDate <= input.EndDate) ||
                          item.Balance == input.Balance);

            //Sorting
            Expression<Func<Client, object>> keySelector = input.Sorting?.ToLower() switch
            {
                "roomnumber" => client => client.RoomNumber,
                "balance" => client => client.Balance,
                "mail" => client => client.User.Mail,
                "number" => client => client.User.Number,
                "personalNumber" => client => client.User.PersonalNumber
            }; ;

            if (!input.isAscending)
                query = query.OrderByDescending(keySelector);
            else
                query = query.OrderBy(keySelector);

            List<Client> result = await query.ToListAsync();

            //Pagination
            result = await Pagination(result, input.SkipCount, input.MaxResultCount);
            return result;
        }


        public virtual async Task<List<Employee>> GetAllEmployeeAsync(GetAllEmployeeInput input)
        {
            //Filtering according input
            IQueryable<Employee> query = _db.Set<Employee>();
            query = query.Where(item =>
                          item.User.PersonalNumber.Contains(input.PersonalNumber) ||
                          item.User.Mail.Contains(input.Mail) ||
                          item.User.Number.Contains(input.Number) ||
                          item.Role.Contains(input.Role) ||
                          (item.User.CreateDate >= input.StartDate && item.User.CreateDate <= input.EndDate));

            //Sorting
            Expression<Func<Employee, object>> keySelector = input.Sorting?.ToLower() switch
            {
                "role" => employee => employee.Role,
                "mail" => employee => employee.User.Mail,
                "number" => employee => employee.User.Number,
                "personalNumber" => client => client.User.PersonalNumber
            }; ;

            if (!input.isAscending)
                query = query.OrderByDescending(keySelector);
            else
                query = query.OrderBy(keySelector);

            List<Employee> result = await query.ToListAsync();

            //Pagination
            result = await Pagination(result, input.SkipCount, input.MaxResultCount);
            return result;
        }


        public virtual async Task<List<T>> Pagination<T>(List<T> allData, int skipCount, int maxResultCount)
        {
            var paginatedData = allData.Skip((skipCount - 1) * maxResultCount)
                                       .Take(maxResultCount)
                                       .ToList();
            return paginatedData;
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            T item = await _db.Set<T>().FindAsync(id);
            return item;

        }

        public virtual async Task<List<T>> UpdateAsync(T item)
        {
            _db.Set<T>().Update(item);
            await _db.SaveChangesAsync();

            var items = await _db.Set<T>().ToListAsync();
            return items;
        }
    }
}
