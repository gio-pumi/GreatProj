using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.Entities;
using GreatProj.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatProj.Core.Repositoy
{
    public class UserRepository<T> : BaseRepository<User>, IUserRepository<User>
    {
        public UserRepository(AppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
