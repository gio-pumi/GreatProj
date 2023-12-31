﻿using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;

namespace GreatProj.Core.Repositoy
{
    public class ClientRepository<T> : BaseRepository<Client>, IClientRepository<Client>
    {
        public ClientRepository(AppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
