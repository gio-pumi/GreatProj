using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly IClientRepository<Client> _clientRepository;
        public ClientService(
            IClientRepository<Client> clientRepository,
            IAuthorizeService authorizeService)
        {
            _clientRepository = clientRepository;
            _authorizeService = authorizeService;
        }
        public async Task<List<ClientDto>> AddClientAsync(ClientAddDto clientAddDto)
        {
            long userId = 0;
            if (await _clientRepository.CheckClientExist(clientAddDto))
            {
                throw new Exception("Client With Same User Already Exist");
            }
            var userRespose = await _authorizeService.Register(clientAddDto.User, clientAddDto.User.Password);
            var clients = await _clientRepository.AddClientAsync(clientAddDto, userRespose.Data);
            return clients;
        }
    }
}


