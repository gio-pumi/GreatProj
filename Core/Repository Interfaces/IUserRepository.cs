using GreatProj.Core.Interfaces;
using GreatProj.Core.Models.ClientDto;
using GreatProj.Core.Models.User;
using GreatProj.Domain.DbEntities;

namespace GreatProj.Core.Repository_Interfaces
{
    public interface IUserRepository<T> : IBaseRepository<User>
    {
        Task<UserDto> CheckUserExist(ClientDto clientDto);
    }
}
