using AutoMapper;
using GreatProj.Core.Models.User;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatProj.Core.Repositoy
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public AuthorizeRepository(AppDbContext DbContext, IMapper mapper)
        {
            _db = DbContext;
            _mapper = mapper;
        }
        public async Task<long> AddUser(UserDto userDto, byte[] passwordHash, byte[] passwordSalt)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }
        public async Task<long> UserExists(string userName)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            if (user != null)
            {
                return user.Id;
            }
            return 0;
        }
        public async Task<User> CheckCredentials(string userName)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            return user;
        }
    }
}
