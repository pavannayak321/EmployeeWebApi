using EmployeeWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApi.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
