using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using wms_api.Entities;

namespace wms_api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNull] DbContextOptions options): base(options)
        {
        }

        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>()
                .HasAlternateKey(u => u.Code);
        }
    }
}
