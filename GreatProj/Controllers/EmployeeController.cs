using AutoMapper;
using GreatProj.Core.Interfaces;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Core.Repositoy;
using GreatProj.Domain.Entities;
using GreatProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreatProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EmployeeController(IEmployeeRepository<Employee> employeeRepository, IMapper mapper, IUserService userService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userService = userService;

        }


        [HttpPost]
        public async Task<List<EmployeeDTO>> AddClient(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            var user = await _userService.CheckUserExistForEmployee(employee);

            if (user != null)
            {
                employee.User = user;

                var employees = await _employeeRepository.AddAsync(employee);
                var employeesDTO = _mapper.Map<List<EmployeeDTO>>(employees);

                return employeesDTO;
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpGet]
        public async Task<List<EmployeeDTO>> GetAllClients()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeesDTO = _mapper.Map<List<EmployeeDTO>>(employees);
            return employeesDTO;
        }

        [HttpGet]
        [Route("id")]
        public async Task<EmployeeDTO> GetClientById(long id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }

        [HttpDelete]

        public async Task<List<EmployeeDTO>> DeleteClient(long id)
        {
            var employee = await _employeeRepository.DeleteAsync(id);
            var employeesDTO = _mapper.Map<List<EmployeeDTO>>(employee);
            return employeesDTO;
        }
    }
}

