using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Orders;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<OrderListItemDto>> GetListAsync(GetOrdersQuery query, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}