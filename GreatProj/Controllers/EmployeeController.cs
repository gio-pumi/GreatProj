using AutoMapper;
using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Employee;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GreatProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EmployeeController(IEmployeeRepository<Employee> employeeRepository, IMapper mapper, IUserService userService, IUserRepository<User> userRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userService = userService;
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<List<EmployeeDTO>> AddClient(EmployeeDTO employeeDTO)
        {
            var employee= _mapper.Map<Employee>(employeeDTO);

            var employyees = await _userService.AddEmployee(employee);
            var employyeesDTO = _mapper.Map<List<EmployeeDTO>>(employyees);

            return employyeesDTO;
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

        [HttpPut]
        public async Task<List<EmployeeUpdateDTO>> UpdateEmployee(EmployeeUpdateDTO employeeDTO)
        {

            var employee = await _employeeRepository.GetByIdAsync(employeeDTO.Id);
            var user = await _userRepository.GetByIdAsync(employee.UserId);

            employee = _mapper.Map<Employee>(employeeDTO);
            employee.UserId = user.Id;
            employee.User = null;

            var employees = await _employeeRepository.UpdateAsync(employee);

            var employeesDTO = _mapper.Map<List<EmployeeUpdateDTO>>(employees);
            return employeesDTO;

        }
    }
}

