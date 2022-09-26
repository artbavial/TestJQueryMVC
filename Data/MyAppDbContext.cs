using Microsoft.EntityFrameworkCore;
using TestJQueryMVC.Models;

namespace TestJQueryMVC.Data
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) {
            
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }

    }
}
