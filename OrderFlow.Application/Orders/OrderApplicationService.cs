using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Application.Common.Pagination;
using OrderFlow.Domain.Common;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Application.Orders;

public sealed class OrderApplicationService(IOrderRepository orderRepository) : IOrderApplicationService
{
    public async Task<OrderDto> CreateAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

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

    public async Task<PagedResult<OrderListItemDto>> GetListAsync(
        GetOrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await orderRepository.GetListAsync(query, cancellationToken);
    }
}