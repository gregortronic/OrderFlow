namespace OrderFlow.Application.Orders;

public sealed record OrderDto(
    Guid Id,
    DateTimeOffset CreatedAtUtc,
    string Status,
    decimal TotalAmount,
    IReadOnlyCollection<OrderItemDto> Items);

public sealed record OrderItemDto(
    Guid Id,
    Guid ProductId,
    string Sku,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice);

public sealed record OrderListItemDto(
    Guid Id,
    DateTimeOffset CreatedAtUtc,
    string Status,
    int ItemsCount,
    decimal TotalAmount);