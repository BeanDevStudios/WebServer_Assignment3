using Assignment_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment_2.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<Employee> Employees { get; set; }
    }
}
