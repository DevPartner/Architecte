using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        // Primary Key
        builder.HasKey(p => p.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        // Properties
        builder.Property(c => c.Type)
            .IsRequired()
            .HasMaxLength(50);
    }
}
