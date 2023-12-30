using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;

namespace GreatProj.Core.Repositoy
{
    public class CountryRepository<T> : BaseRepository<Country>, ICountryRepository<Country>
    {
        public CountryRepository(AppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
