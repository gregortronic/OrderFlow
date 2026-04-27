using OrderFlow.Domain.Orders;

namespace OrderFlow.Application.Orders;

internal static class OrderMappings
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            Id: order.Id,
            CreatedAtUtc: order.CreatedAtUtc,
            Status: order.Status.ToString(),
            TotalAmount: order.Items.Sum(x => x.TotalPrice),
            Items: order.Items
                .Select(x => x.ToDto())
                .ToList());
    }

    public static OrderItemDto ToDto(this OrderItem item)
    {
        return new OrderItemDto(
            Id: item.Id,
            ProductId: item.ProductId,
            Sku: item.Sku,
            ProductName: item.ProductName,
            Quantity: item.Quantity,
            UnitPrice: item.UnitPrice,
            TotalPrice: item.TotalPrice);
    }
    
    public static OrderListItemDto ToListItemDto(this Order order)
    {
        return new OrderListItemDto(
            Id: order.Id,
            CreatedAtUtc: order.CreatedAtUtc,
            Status: order.Status.ToString(),
            ItemsCount: order.Items.Count,
            TotalAmount: order.Items.Sum(x => x.TotalPrice));
    }
}