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
            IQueryable<Client> query = _db.Set<Client>().Include(u => u.User);
            if(input.PersonalNumber != null || 
                input.Mail != null|| 
                input.Number != null||
                input.RoomNumber != null||
                input.Balance != null||
                input.StartDate != null ||
                input.EndDate != null )
            {
                query = query.Where(item =>
                                    item.User.PersonalNumber.Contains(input.PersonalNumber) ||
                                    item.User.Mail.Contains(input.Mail) ||
                                    item.User.Number.Contains(input.Number) ||
                                    (item.User.CreateDate >= input.StartDate && item.User.CreateDate <= input.EndDate) ||
                                    item.RoomNumber.Contains(input.RoomNumber) ||
                                    item.Balance == input.Balance);
            }
            //Sorting
            if (input.Sorting != null)
            {
                Expression<Func<Client, object>> keySelector = input.Sorting?.ToLower() switch
                {
                    "roomnumber" => client => client.RoomNumber,
                    "balance" => client => client.Balance,
                    "mail" => client => client.User.Mail,
                    "number" => client => client.User.Number,
                    "personalNumber" => client => client.User.PersonalNumber,
                    _ => throw new NotImplementedException()
                };
                if (!input.IsAscending)
                    query = query.OrderByDescending(keySelector);
                else
                    query = query.OrderBy(keySelector);
            }
            List<Client> result = await query.ToListAsync();
            //Pagination
            result = Pagination(result, input.SkipCount, input.MaxResultCount);
            return result;
        }
        public virtual async Task<List<Employee>> GetAllEmployeeAsync(GetAllEmployeeInput input)
        {
            //Filtering according input
            IQueryable<Employee> employeeQuery = _db.Set<Employee>();
            IQueryable<User> userQuery = _db.Set<User>();
            var employeeAndUserJoinQuery = (from emp in employeeQuery
                             join user in userQuery 
                             on emp.UserId equals user.Id
                             select new Employee
                             {
                                Id = emp.Id,
                                Role = emp.Role,
                                User = user
                             });
            if (input.PersonalNumber != null ||
                input.Mail != null ||
                input.Number != null ||
                input.Role != null ||
                input.StartDate != null ||
                input.EndDate != null)
            {
                employeeAndUserJoinQuery =  employeeAndUserJoinQuery.Where(item =>
                                                                           item.User.PersonalNumber.Contains(input.PersonalNumber) ||
                                                                           item.User.Mail.Contains(input.Mail) ||
                                                                           item.User.Number.Contains(input.Number) ||
                                                                           item.Role.Contains(input.Role) ||
                                                                           (item.User.CreateDate >= input.StartDate && item.User.CreateDate <= input.EndDate));
            }
            //Sorting
            if (input.Sorting != null)
            {
                Expression<Func<Employee, object>> keySelector = input.Sorting?.ToLower() switch
                {
                    "role" => employee => employee.Role,
                    "mail" => employee => employee.User.Mail,
                    "number" => employee => employee.User.Number,
                    "personalNumber" => client => client.User.PersonalNumber,
                    _ => throw new NotImplementedException()
                };
                if (!input.IsAscending)
                    employeeAndUserJoinQuery = employeeAndUserJoinQuery.OrderByDescending(keySelector);
                else
                    employeeAndUserJoinQuery = employeeAndUserJoinQuery.OrderBy(keySelector);
            }
            List<Employee> result = await employeeAndUserJoinQuery.ToListAsync();
            //Pagination
            result = Pagination(result, input.SkipCount, input.MaxResultCount);
            return result;
        }
        public virtual List<T> Pagination<T>(List<T> allData, int skipCount, int maxResultCount)
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
