using OrderFlow.Application.Common.Pagination;

namespace OrderFlow.Application.Orders;

public interface IOrderApplicationService
{
    Task<OrderDto> CreateAsync(CreateOrderCommand command, CancellationToken cancellationToken = default);

    Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<OrderDto> CancelAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<OrderListItemDto>> GetListAsync(
        GetOrdersQuery query,
        CancellationToken cancellationToken = default);
}