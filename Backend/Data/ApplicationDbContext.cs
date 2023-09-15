using EmployeesBE.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesBE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    Name = "Arturo",
                    LastName = "Martínez",
                });
        }

        /*
         Migrations:
        add-migration AddEmployeesTable
        update-database
        add-migration SeedEmployeesTable
        update-database
         */
    }
}
