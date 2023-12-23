using AutoMapper;
using GreatProj.Core.Models.ClientDTO;
using GreatProj.Core.Models.Employee;
using GreatProj.Domain.Entities;
using GreatProj.Models;

namespace GreatProj.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Client,ClientDTO>().ReverseMap();
            CreateMap<Client,ClientUpdateDTO>().ReverseMap();
            CreateMap<Employee,EmployeeDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Employee,EmployeeUpdateDTO>().ReverseMap();
        }
    }
}
