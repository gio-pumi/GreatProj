using AutoMapper;
using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDTO;
using GreatProj.Core.Models.Paging;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GreatProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository<Client> _clientRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public ClientController(IClientRepository<Client> clientRepository, IUserRepository<User> userRepository, IMapper mapper, IUserService userService)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;

        }

        [HttpPost]
        public async Task<List<ClientDTO>> AddClient(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);

            var clients = await _userService.AddClient(client);
            var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);

            return clientsDTO;
        }

        [HttpGet]
        public async Task<PagedResultDTO<ClientDTO>> GetAllClients ([FromQuery] GetAllClientInput input)
        {
            var clients = await _clientRepository.GetAllClientAsync(input);
            var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);

            var result = new PagedResultDTO<ClientDTO>();
            result.Count = clientsDTO.Count;
            result.Items = clientsDTO;

            return result;
        }

        [HttpGet]
        [Route("id")]
        public async Task<ClientDTO> GetClientById(long id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            var clientDTO = _mapper.Map<ClientDTO>(client);
            return clientDTO;
        }

        [HttpDelete]

        public async Task<List<ClientDTO>> DeleteClient(long id)
        {
            var clients = await _clientRepository.DeleteAsync(id);
            var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
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
