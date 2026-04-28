using OrderFlow.Domain.Common;

namespace OrderFlow.Domain.Orders;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    private Order()
    {
    }

    public Guid Id { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public static Order Create(IEnumerable<OrderItemData> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        var materializedItems = items.ToList();
        if (materializedItems.Count == 0)
        {
            throw new DomainException("Order must contain at least one item.");
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAtUtc = DateTimeOffset.UtcNow,
            Status = OrderStatus.Draft
        };

        foreach (var item in materializedItems)
        {
            order.AddItem(item.ProductId, item.Sku, item.ProductName, item.Quantity, item.UnitPrice);
        }

        return order;
    }

    public void AddItem(Guid productId, string sku, string productName, int quantity, decimal unitPrice)
    {
        if (Status != OrderStatus.Draft)
        {
            throw new DomainException("Items can be added only to draft orders.");
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

        _items.Add(OrderItem.Create(
            orderId: Id,
            productId: productId,
            sku: sku.Trim(),
            productName: productName.Trim(),
            quantity: quantity,
            unitPrice: unitPrice));
    }

    public void Cancel()
    {
        switch (Status)
        {
            case OrderStatus.Cancelled:
                return;
            case OrderStatus.Reserved:
                throw new DomainException("Reserved orders cannot be cancelled before releasing reservation.");
            default:
                Status = OrderStatus.Cancelled;
                break;
        }
    }
}