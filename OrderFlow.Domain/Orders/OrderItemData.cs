namespace OrderFlow.Domain.Orders;

public sealed record OrderItemData(
    Guid ProductId,
    string Sku,
    string ProductName,
    int Quantity,
    decimal UnitPrice);