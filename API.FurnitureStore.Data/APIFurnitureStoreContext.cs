
using API.FurnitureStore.shared;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.Data
{
    public class APIFurnitureStoreContext : DbContext
    {
        public APIFurnitureStoreContext(DbContextOptions option) : base(option) { }

        public DbSet<Client> Clients { get; set; }


        public DbSet<Order> orders { get; set; }

        public DbSet<ProductCategory> productCategories { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(e => new { e.OrderId, e.ProductId });
        }



    }
}

