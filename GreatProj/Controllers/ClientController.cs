using AutoMapper;
using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Models.ClientDTO;
using GreatProj.Core.Models.Paging;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreatProj.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository<Client> _clientRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public ClientController(
            IClientRepository<Client> clientRepository,
            IUserRepository<User> userRepository,
            IMapper mapper,
            IClientService clientService)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<List<ClientDto>> AddClient(ClientAddDto clientAddDto)
        {
            var clients = await _clientService.AddClientAsync(clientAddDto);
            var clientDto = _mapper.Map<List<ClientDto>>(clients);
            return clientDto;
        }

        [HttpGet]
        public async Task<PagedResultDTO<ClientDto>> GetAllClients([FromQuery] GetAllClientInput input)
        {
            var clientsDTO = await _clientRepository.GetAllClientAsync(input);
            var result = new PagedResultDTO<ClientDto>
            {
                Count = clientsDTO.Count,
                Items = clientsDTO
            };
            return result;
        }

        [HttpGet]
        public async Task<ClientDto> GetCurrentClientInfo()
        {
            string userId = HttpContext.User.FindFirstValue("UserId");
            var clientDto = await _clientRepository.GetCurrentClientInfoAsync(Convert.ToInt64(userId));
            return clientDto;
        }

        [HttpGet]
        public async Task<ClientDto> GetClientById(long id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            var clientDTO = _mapper.Map<ClientDto>(client);
            return clientDTO;
        }

        [HttpDelete]
        public async Task<List<ClientDto>> DeleteClient(long id)
        {
            var clients = await _clientRepository.DeleteAsync(id);
            var clientsDTO = _mapper.Map<List<ClientDto>>(clients);
            return clientsDTO;
        }

        [HttpPut]
        public async Task<List<ClientUpdateDTO>> UpdateClient(ClientUpdateDTO clientDTO)
        {
            var client = await _clientRepository.GetByIdAsync(clientDTO.Id);
            var user = await _userRepository.GetByIdAsync(client.UserId);
            client = _mapper.Map<Client>(clientDTO);
            client.UserId = user.Id;
            client.User = user;
            var clients = await _clientRepository.UpdateAsync(client);
            var clientsDTO = _mapper.Map<List<ClientUpdateDTO>>(clients);
            return clientsDTO;
        }
    }
}
