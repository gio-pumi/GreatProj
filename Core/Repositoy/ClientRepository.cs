using AutoMapper;
using GreatProj.Core.Models.Client;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatProj.Core.Repositoy
{
    public class ClientRepository<T> : BaseRepository<Client>, IClientRepository<Client>
    {
        private readonly AppDbContext _db;
        public IMapper _mapper;

        public ClientRepository(AppDbContext DbContext,
                                IMapper mapper) : base(DbContext)
        {
            _db = DbContext;
            _mapper = mapper;
        }
        public async Task<bool> CheckClientExist(ClientAddDto clientAddDto)
        {
            return await _db.Clients.Include(c => c.User).AnyAsync(c => c.User.UserName == clientAddDto.User.UserName || c.User.PersonalNumber == clientAddDto.User.PersonalNumber);
        }
        public virtual async Task<List<ClientDto>> AddClientAsync(ClientAddDto clientAddDto, long userId)
        {
            var client = _mapper.Map<Client>(clientAddDto);
            client.UserId = userId;
            client.User = null;
            _db.Set<Client>().Add(client);
            await _db.SaveChangesAsync();
            var clients = await _db.Clients.Include(c=> c.User).Include(c => c.Country).ToListAsync();
            var clientsDto = _mapper.Map<List<ClientDto>>(clients);
            return clientsDto;
        }
        public async Task<ClientDto> GetCurrentClientInfoAsync(long userId)
        {
            var client = await _db.Set<Client>().FirstOrDefaultAsync(c => c.UserId == userId);
            var currentClient = _mapper.Map<ClientDto>(client);
            return currentClient;
        }
    }
}
