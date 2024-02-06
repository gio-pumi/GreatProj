using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;

namespace GreatProj.Core.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> AddClientAsync(ClientAddDto client);
    }
}
