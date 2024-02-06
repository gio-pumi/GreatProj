using AutoMapper;
using GreatProj.Core.Models.Country;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatProj.Core.Repositoy
{
    public class CountryRepository<T> : BaseRepository<Country>, ICountryRepository<Country>
    {
        private readonly AppDbContext _db;
        public IMapper _mapper { get; }

        public CountryRepository(AppDbContext DbContext,
                                 IMapper mapper) : base(DbContext)
        {
            _db = DbContext;
            _mapper = mapper;
        }

        public virtual async Task<List<CountryDto>> AddCountryAsync(CountryDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            _db.Set<Country>().Add(country);
            await _db.SaveChangesAsync();
            var countries = await _db.Set<Country>().ToListAsync();
            var countriesDto = _mapper.Map<List<CountryDto>>(countries);
            return countriesDto;
        }
    }
}
