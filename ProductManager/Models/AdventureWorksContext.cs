using Microsoft.EntityFrameworkCore;

namespace ProductManager.Models
{
    public partial class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
            : base(options)
        { }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Production");

            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .Property(e => e.ProductId)
                    .HasColumnName("ProductID");
            });
        }
    }
}
