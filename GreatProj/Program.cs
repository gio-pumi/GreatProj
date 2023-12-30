using GreatProj.Core;
using GreatProj.Core.Interfaces;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Core.Repositoy;
using GreatProj.Core.Services;
using GreatProj.Domain.DbEntities;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatProj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
             });

            builder.Services.AddControllers();

            //Dependency Injection mapper
            builder.Services.AddScoped<IClientRepository<Client>, ClientRepository<Client>>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEmployeeRepository<Employee>, EmployeeRepository<Client>>();
            builder.Services.AddScoped<IUserRepository<User>, UserRepository<User>>();
            builder.Services.AddScoped<ICountryRepository<Country>, CountryRepository<Country>>();

            // Add Automaper Configuration
            builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
