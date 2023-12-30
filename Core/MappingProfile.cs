using AutoMapper;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDTO;
using GreatProj.Core.Models.Country;
using GreatProj.Core.Models.Employee;
using GreatProj.Core.Models.Translation;
using GreatProj.Domain.DbEntities;
using GreatProj.Models;

namespace GreatProj.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientAddDTO>().ReverseMap();
            CreateMap<Client, ClientUpdateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CountryUpdateDTO>().ReverseMap();
            CreateMap<Translation, TranslationDTO>().ReverseMap();
        }
    }
}
