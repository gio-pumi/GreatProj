using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Country;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Repository_Interfaces
{
    public interface ICountryRepository<T> : IBaseRepository<Country>
    {
        Task<List<CountryDto>> AddCountryAsync(CountryDto countryDto);
    }
}
