using OrderFlow.Domain.Common;

namespace OrderFlow.Domain.Orders;

public sealed class OrderItem
{
    private OrderItem()
    {
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string Sku { get; private set; } = string.Empty;

    public string ProductName { get; private set; } = string.Empty;

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    internal static OrderItem Create(
        Guid orderId,
        Guid productId,
        string sku,
        string productName,
        int quantity,
        decimal unitPrice)
    {
        if (orderId == Guid.Empty)
        {
            throw new DomainException("OrderId must not be empty.");
        }

        if (productId == Guid.Empty)
        {
            throw new DomainException("ProductId must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new DomainException("Sku is required.");
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new DomainException("ProductName is required.");
        }

        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        if (unitPrice <= 0)
        {
            throw new DomainException("UnitPrice must be greater than zero.");
        }

        return new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = productId,
            Sku = sku,
            ProductName = productName,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }
}