using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleShopWebApi.Data.Entities;

namespace SampleShopWebApi.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.ImgUri)
                .IsRequired();
            builder.Property(t => t.Name)
                .IsRequired();
            builder.Property(t => t.Price)
                .IsRequired();
            builder.Property(t => t.Description)
                .IsRequired(false);
        }
    }
}