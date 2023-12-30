using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
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
