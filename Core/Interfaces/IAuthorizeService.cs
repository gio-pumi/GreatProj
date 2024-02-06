using GreatProj.Core.Models;
using GreatProj.Core.Models.User;

namespace GreatProj.Core.Interfaces
{
    public interface IAuthorizeService
    {
        Task<ServiceResponse<long>> Register (UserDto user, string password);
        Task<ServiceResponse<string>> login (string userName, string password);
    }
}
