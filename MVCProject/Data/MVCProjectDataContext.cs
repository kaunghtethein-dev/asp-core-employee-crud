using Microsoft.EntityFrameworkCore;
using MVCProject.Models.Domain;

namespace MVCProject.Data
{
    public class MVCProjectDataContext:DbContext
    {
        public MVCProjectDataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
