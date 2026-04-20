namespace OrderFlow.Application.Orders;

public interface IOrderApplicationService
{
    Task<OrderDto> CreateAsync(CreateOrderCommand command, CancellationToken cancellationToken = default);
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OrderListItemDto>> GetListAsync(CancellationToken cancellationToken = default);
}