using AutoMapper;
using GreatProj.Core.Models.Country;
using GreatProj.Core.Models.Paging;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace GreatProj.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository<Country> _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(
            ICountryRepository<Country> countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<List<CountryDTO>> AddCountry(CountryDTO countryDTO)
        {
            var country = _mapper.Map<Country>(countryDTO);
            var countries = await _countryRepository.AddAsync(country);
            var countriesDTO = _mapper.Map<List<CountryDTO>>(countries);
            return countriesDTO;
        }

        [HttpGet]
        public async Task<PagedResultDTO<CountryDTO>> GetCountries([FromQuery] GetAllCountryInput input)
        {
            var countriesDTO = await _countryRepository.GetAllCountryAsync(input);
            var result = new PagedResultDTO<CountryDTO>
            {
                Count = countriesDTO.Count,
                Items = countriesDTO
            };
            return result;
        }

        [HttpGet]
        public async Task<CountryDTO> GetCountriesById(long id)
        {
            var countryDTO = await _countryRepository.GetByIdAsync(id);
            var result = _mapper.Map<CountryDTO>(countryDTO);
            return result;
        }

        [HttpPut]
        public async Task<List<CountryDTO>> EditCountry(CountryUpdateDTO countryDTO)
        {
            var country = _mapper.Map<Country>(countryDTO);
            var countries = await _countryRepository.UpdateAsync(country);
            var countriesDTO = _mapper.Map<List<CountryDTO>>(countries);
            return countriesDTO;
        }

        [HttpDelete]
        public async Task<List<CountryDTO>> DeleteCountry(long id)
        {
            var countries = await _countryRepository.DeleteAsync(id);
            var countryDTO = _mapper.Map<List<CountryDTO>>(countries);
            return countryDTO;
        }
    }
}
