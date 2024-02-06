using GreatProj.Core.Models.User;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Repository_Interfaces
{
    public interface IAuthorizeRepository
    {
        Task<long> AddUser(UserDto userDto, byte[] passwordHash, byte[] passwordSalt);
        Task<long> UserExists(string userName);
        Task<User> CheckCredentials(string user);
    }
}
