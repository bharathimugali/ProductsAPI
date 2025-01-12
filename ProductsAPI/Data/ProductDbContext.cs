using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data.Entities;

namespace ProductsAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sequence is defined to generate unique 6-digit product id
            modelBuilder.HasSequence<int>("ProductIdSequence")
                .StartsAt(100000)
                .IncrementsBy(1)
                .HasMin(100000)
                .HasMax(999999);

            // Configured the Product entity to use the sequence
            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEXT VALUE FOR ProductIdSequence");
        }

    }
}
