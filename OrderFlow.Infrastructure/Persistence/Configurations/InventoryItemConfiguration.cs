using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Inventory;

namespace OrderFlow.Infrastructure.Persistence.Configurations;

public sealed class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("inventory_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Sku)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.ProductName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.QuantityOnHand)
            .IsRequired();

        builder.Property(x => x.ReservedQuantity)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.UpdatedAtUtc)
            .IsRequired();

        builder.Ignore(x => x.AvailableQuantity);

        builder.HasIndex(x => x.Sku)
            .IsUnique();
    }
}