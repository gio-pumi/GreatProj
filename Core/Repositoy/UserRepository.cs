using AutoMapper;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Models.User;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatProj.Core.Repositoy
{
    public class UserRepository<T> : BaseRepository<User>, IUserRepository<User>
    {
        public UserRepository(AppDbContext DbContext, IMapper mapper) : base(DbContext)
        {
            _db = DbContext;
           _mapper = mapper;
        }

        public AppDbContext _db { get; }
        private readonly IMapper _mapper;

        public async Task<UserDto> CheckUserExist(ClientDto clientDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(c => c.PersonalNumber == clientDto.User.PersonalNumber);
            return _mapper.Map<UserDto>(user);
        }
    }
}
