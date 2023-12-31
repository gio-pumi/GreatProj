﻿using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;

namespace GreatProj.Core.Repositoy
{
    public class EmployeeRepository<T> : BaseRepository<Employee>, IEmployeeRepository<Employee>
    {
        public EmployeeRepository(AppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
