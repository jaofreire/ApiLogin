using ApiLogin.Infraestructure.Data.Map;
using ApiLogin.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiLogin.Infraestructure.Data
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
