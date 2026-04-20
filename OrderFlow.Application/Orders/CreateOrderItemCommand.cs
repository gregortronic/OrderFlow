namespace OrderFlow.Application.Orders;

public sealed record CreateOrderCommand(IReadOnlyCollection<CreateOrderItemCommand> Items);

public sealed record CreateOrderItemCommand(
    Guid ProductId,
    string Sku,
    string ProductName,
    int Quantity,
    decimal UnitPrice);