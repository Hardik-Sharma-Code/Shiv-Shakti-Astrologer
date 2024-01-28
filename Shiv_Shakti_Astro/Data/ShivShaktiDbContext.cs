using Microsoft.EntityFrameworkCore;
using Shiv_Shakti_Astro.Controllers;
using Shiv_Shakti_Astro.Models;

namespace Shiv_Shakti_Astro.Data
{
    public class ShivShaktiDbContext:DbContext
    {
        public ShivShaktiDbContext(DbContextOptions<ShivShaktiDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Customer> Customers { get; set; }

    }
}
