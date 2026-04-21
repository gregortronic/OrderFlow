namespace OrderFlow.Api.Contracts.Orders;

public sealed record OrderResponse(
    Guid Id,
    DateTimeOffset CreatedAtUtc,
    string Status,
    decimal TotalAmount,
    IReadOnlyCollection<OrderItemResponse> Items);

public sealed record OrderItemResponse(
    Guid Id,
    Guid ProductId,
    string Sku,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice);

public sealed record OrderListItemResponse(
    Guid Id,
    DateTimeOffset CreatedAtUtc,
    string Status,
    int ItemsCount,
    decimal TotalAmount);