using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Repository_Interfaces
{
    public interface IClientRepository<T> : IBaseRepository<Client>
    {
        Task<ClientDto> GetCurrentClientInfoAsync(long userId);
        Task<bool> CheckClientExist(ClientAddDto client);
        Task<List<ClientDto>> AddClientAsync(ClientAddDto clientDto,long userId);
    }
}
