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
        private readonly ICountryRepository<CountryDto> _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(
            ICountryRepository<CountryDto> countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<List<CountryDto>> AddCountry(CountryDto countryDTO)
        {
            var countries = await _countryRepository.AddCountryAsync(countryDTO);
            var countriesDTO = _mapper.Map<List<CountryDto>>(countries);
            return countriesDTO;
        }

        [HttpGet]
        public async Task<PagedResultDTO<CountryDto>> GetCountries([FromQuery] GetAllCountryInput input)
        {
            var countriesDTO = await _countryRepository.GetAllCountryAsync(input);
            var result = new PagedResultDTO<CountryDto>
            {
                Count = countriesDTO.Count,
                Items = countriesDTO
            };
            return result;
        }

        [HttpGet]
        public async Task<CountryDto> GetCountriesById(long id)
        {
            var countryDTO = await _countryRepository.GetByIdAsync(id);
            var result = _mapper.Map<CountryDto>(countryDTO);
            return result;
        }

        [HttpPut]
        public async Task<List<CountryDto>> EditCountry(CountryUpdateDTO countryUpdateDto)
        {
            var countryDto = _mapper.Map<Country>(countryUpdateDto);
            var countries = await _countryRepository.UpdateAsync(countryDto);
            var countriesDTO = _mapper.Map<List<CountryDto>>(countries);
            return countriesDTO;
        }

        [HttpDelete]
        public async Task<List<CountryDto>> DeleteCountry(long id)
        {
            var countries = await _countryRepository.DeleteAsync(id);
            var countryDTO = _mapper.Map<List<CountryDto>>(countries);
            return countryDTO;
        }
    }
}
