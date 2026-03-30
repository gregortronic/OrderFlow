using OrderFlow.Domain.Common;

namespace OrderFlow.Domain.Inventory;

public sealed class InventoryItem
{
    private InventoryItem()
    {
    }

    public Guid Id { get; private set; }

    public string Sku { get; private set; } = string.Empty;

    public string ProductName { get; private set; } = string.Empty;

    public int QuantityOnHand { get; private set; }

    public int ReservedQuantity { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public DateTimeOffset UpdatedAtUtc { get; private set; }

    public int AvailableQuantity => QuantityOnHand - ReservedQuantity;

    public static InventoryItem Create(string sku, string productName, int initialQuantity)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new DomainException("Sku is required.");
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new DomainException("ProductName is required.");
        }

        if (initialQuantity < 0)
        {
            throw new DomainException("Initial quantity must not be negative.");
        }

        var now = DateTimeOffset.UtcNow;

        return new InventoryItem
        {
            Id = Guid.NewGuid(),
            Sku = sku.Trim(),
            ProductName = productName.Trim(),
            QuantityOnHand = initialQuantity,
            ReservedQuantity = 0,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Reserve quantity must be greater than zero.");
        }

        if (AvailableQuantity < quantity)
        {
            throw new DomainException("Not enough available stock to reserve.");
        }

        ReservedQuantity += quantity;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void Release(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Release quantity must be greater than zero.");
        }

        if (ReservedQuantity < quantity)
        {
            throw new DomainException("Cannot release more than reserved quantity.");
        }

        ReservedQuantity -= quantity;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void Restock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Restock quantity must be greater than zero.");
        }

        QuantityOnHand += quantity;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }
}