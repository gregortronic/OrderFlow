using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Domain.Common;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Application.Orders;

public sealed class OrderApplicationService(IOrderRepository orderRepository) : IOrderApplicationService
{
    public async Task<OrderDto> CreateAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (command.Items.Count == 0)
        {
            throw new DomainException("Order must contain at least one item.");
        }

        var order = Order.Create(command.Items.Select(x =>
            new OrderItemData(
                x.ProductId,
                x.Sku,
                x.ProductName,
                x.Quantity,
                x.UnitPrice)));

        await orderRepository.AddAsync(order, cancellationToken);
        await orderRepository.SaveChangesAsync(cancellationToken);

        return order.ToDto();
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken);
        return order?.ToDto();
    }

    public async Task<IReadOnlyList<OrderListItemDto>> GetListAsync(CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetListAsync(cancellationToken);
        return orders.Select(x => x.ToListItemDto()).ToList();
    }
}

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
                .Select(x => new OrderItemDto(
                    Id: x.Id,
                    ProductId: x.ProductId,
                    Sku: x.Sku,
                    ProductName: x.ProductName,
                    Quantity: x.Quantity,
                    UnitPrice: x.UnitPrice,
                    TotalPrice: x.TotalPrice))
                .ToList());
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