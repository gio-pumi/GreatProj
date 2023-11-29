using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.Entities;
using GreatProj.Infrastructure.Data;

namespace GreatProj.Core.Repositoy
{
    public class UserRepository<T> : BaseRepository<User>, IUserRepository<User>
    {
        public UserRepository(AppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
