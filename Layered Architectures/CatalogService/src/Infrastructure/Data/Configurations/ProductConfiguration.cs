using System.Reflection.Emit;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        // Primary Key
        builder.HasKey(p => p.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        // Properties
        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.Image).HasMaxLength(2048); // Assuming image URI will be stored as string

        builder.OwnsOne(product => product.Price).Property(price => price.Price).HasColumnName("Price").HasColumnType("decimal(18,2)"); ;
        builder.OwnsOne(product => product.Price).Property(price => price.Currency).HasColumnName("Currency");

        builder.Property(p => p.Amount).IsRequired().HasColumnType("int"); ;

        // Relationships
        builder.HasOne(p => p.Category)
               .WithMany()
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
