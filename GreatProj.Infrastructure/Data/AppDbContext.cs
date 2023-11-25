using GreatProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreatProj.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Client> Clients { get; set; }
            public  DbSet<Employee> Employees { get; set; }
            public  DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(client => client.User)
                .WithMany()
                .HasForeignKey(client => client.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        
    }
}
