using Microsoft.EntityFrameworkCore;
using SampleShopWebApi.Data.Configurations;
using SampleShopWebApi.Data.Entities;

namespace SampleShopWebApi.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            // nothing
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}