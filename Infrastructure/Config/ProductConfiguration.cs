using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.Property(b => b.ProductUrl).IsRequired();


            builder.HasOne(b=>b.ProductBrand).WithMany().HasForeignKey(b=>b.ProductBrandId);
            builder.HasOne(b=>b.ProductType).WithMany().HasForeignKey(b=>b.ProductTypeId);


        }
    }
}