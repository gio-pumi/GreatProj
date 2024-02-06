using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Models.Country;
using GreatProj.Core.Models.Employee;
using GreatProj.Core.Models.Translation;
using GreatProj.Core.Models.User;
using GreatProj.Domain.DbEntities;
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
        public virtual async Task<List<ClientDto>> GetAllClientAsync(GetAllClientInput input)
        {
            //Filtering according input
            IQueryable<Client> clientQuery = _db.Set<Client>().Include(c => c.Country).Include(u => u.User);
            var mappedClientQuery = from client in clientQuery
                                    select new ClientDto
                                    {
                                        Id = client.Id,
                                        Balance = client.Balance,
                                        RoomNumber = client.RoomNumber,
                                        Country = new CountryDto
                                        {
                                            Id = client.Country.Id,
                                            Code = client.Country.Code,
                                            Name = client.Country.Name,
                                            Translation = new TranslationDTO
                                            {
                                                Id = client.Country.Translation.Id,
                                                Name = client.Country.Translation.Name,
                                                Description = client.Country.Translation.Description
                                            }
                                        },
                                        User = new UserDto
                                        {
                                            Id = client.User.Id,
                                            Mail = client.User.Mail,
                                            Number = client.User.Number,
                                            PersonalNumber = client.User.PersonalNumber
                                        }
                                    };
            if (input.PersonalNumber != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.User.PersonalNumber.Contains(input.PersonalNumber));
            if (input.Mail != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.User.Mail.Contains(input.Mail));
            if (input.Number != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.User.Number.Contains(input.Number));
            if (input.RoomNumber != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.RoomNumber.Contains(input.RoomNumber));
            if (input.Balance != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.Balance == input.Balance);
            if (input.Language != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.Country.Translation.Name == input.Language);
            if (input.StartDate != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.User.CreateDate >= input.StartDate);
            if (input.EndDate != null)
                mappedClientQuery = mappedClientQuery.Where(item => item.User.CreateDate <= input.EndDate);

            //Sorting
            if (input.Sorting != null)
            {
                Expression<Func<ClientDto, object>> keySelector = input.Sorting?.ToLower() switch
                {
                    "personalNumber" => client => client.User.PersonalNumber,
                    "mail" => client => client.User.Mail,
                    "number" => client => client.User.Number,
                    "roomnumber" => client => client.RoomNumber,
                    "balance" => client => client.Balance,
                    _ => throw new NotImplementedException()
                };
                if (!input.IsAscending)
                    mappedClientQuery = mappedClientQuery.OrderByDescending(keySelector);
                else
                    mappedClientQuery = mappedClientQuery.OrderBy(keySelector);
            }
            List<ClientDto> result = await mappedClientQuery.ToListAsync();

            //Pagination
            result = Pagination(result, input.SkipCount, input.MaxResultCount);
            return result;
        }
        public virtual async Task<List<EmployeeDTO>> GetAllEmployeeAsync(GetAllEmployeeInput input)
        {
            //Filtering according input
            IQueryable<Employee> employeeQuery = _db.Set<Employee>();
            IQueryable<User> userQuery = _db.Set<User>();
            var employeeAndUserJoinQuery = from emp in employeeQuery
                                           join user in userQuery
                                           on emp.UserId equals user.Id
                                           select new EmployeeDTO
                                           {
                                               Id = emp.Id,
                                               Role = emp.Role,
                                               User = new UserDto
                                               {
                                                   Id = user.Id,
                                                   Mail = user.Mail,
                                                   Number = user.Number,
                                                   PersonalNumber = user.PersonalNumber,
                                               }
                                           };
            if (input.PersonalNumber != null)
                employeeAndUserJoinQuery = employeeAndUserJoinQuery.Where(item => item.User.PersonalNumber.Contains(input.PersonalNumber));
            if (input.Mail != null)
                employeeAndUserJoinQuery = employeeAndUserJoinQuery.Where(item => item.User.Mail.Contains(input.Mail));
            if (input.Number != null)
                employeeAndUserJoinQuery = employeeAndUserJoinQuery.Where(item => item.User.Number.Contains(input.Number));
            if (input.Role != null)
                employeeAndUserJoinQuery = employeeAndUserJoinQuery.Where(item => item.Role.Contains(input.Role));
            if (input.StartDate != null)
                employeeAndUserJoinQuery = employeeAndUserJoinQuery.Where(item => item.User.CreateDate >= input.StartDate);
            if (input.EndDate != null)
                employeeAndUserJoinQuery = employeeAndUserJoinQuery.Where(item => item.User.CreateDate <= input.EndDate);
            //Sorting
            if (input.Sorting != null)
            {
                Expression<Func<EmployeeDTO, object>> keySelector = input.Sorting?.ToLower() switch
                {
                    "mail" => employee => employee.User.Mail,
                    "number" => employee => employee.User.Number,
                    "role" => employee => employee.Role,
                    "personalNumber" => client => client.User.PersonalNumber,
                    _ => throw new NotImplementedException()
                };
                if (!input.IsAscending)
                    employeeAndUserJoinQuery = employeeAndUserJoinQuery.OrderByDescending(keySelector);
                else
                    employeeAndUserJoinQuery = employeeAndUserJoinQuery.OrderBy(keySelector);
            }
            List<EmployeeDTO> result = await employeeAndUserJoinQuery.ToListAsync();
            //Pagination
            result = Pagination(result, input.SkipCount, input.MaxResultCount);
            return result;
        }
        public virtual async Task<List<CountryDto>> GetAllCountryAsync(GetAllCountryInput input)
        {
            //Filtering according input
            IQueryable<Country> countryQuery = _db.Set<Country>().Include(t => t.Translation);
            var mappedCountryQuery = from country in countryQuery
                                     select new CountryDto
                                     {
                                         Id = country.Id,
                                         Code = country.Code,
                                         Name = country.Name,
                                         Translation = new TranslationDTO
                                         {
                                             Id = country.Translation.Id,
                                             Name = country.Translation.Name,
                                             Description = country.Translation.Description
                                         }
                                     };
            if (input.Code != null)
                mappedCountryQuery = mappedCountryQuery.Where(item => item.Code.Contains(input.Code));
            if (input.Name != null)
                mappedCountryQuery = mappedCountryQuery.Where(item => item.Name.Contains(input.Name));
            //Sorting
            if (input.Sorting != null)
            {
                Expression<Func<CountryDto, object>> keySelector = input.Sorting?.ToLower() switch
                {
                    "code" => country => country.Code,
                    "name" => country => country.Name,
                    _ => throw new NotImplementedException()
                };
                if (!input.IsAscending)
                    mappedCountryQuery = mappedCountryQuery.OrderByDescending(keySelector);
                else
                    mappedCountryQuery = mappedCountryQuery.OrderBy(keySelector);
            }
            List<CountryDto> result = await mappedCountryQuery.ToListAsync();

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
