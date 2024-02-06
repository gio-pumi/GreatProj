using AutoMapper;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Models.ClientDTO;
using GreatProj.Core.Models.Country;
using GreatProj.Core.Models.Employee;
using GreatProj.Core.Models.Translation;
using GreatProj.Core.Models.User;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Client, ClientAddDto>().ReverseMap();
            CreateMap<Client, ClientUpdateDTO>().ReverseMap();
            CreateMap<UserDto, ClientAddDto>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CountryUpdateDTO>().ReverseMap();
            CreateMap<Translation, TranslationDTO>().ReverseMap();
        }
    }
}
