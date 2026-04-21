using OrderFlow.Api.Contracts.Orders;
using OrderFlow.Application.Orders;

namespace OrderFlow.Api.Mapping;

public static class OrderContractMappings
{
    public static CreateOrderCommand ToCommand(this CreateOrderRequest request)
    {
        return new CreateOrderCommand(
            request.Items
                .Select(x => new CreateOrderItemCommand(
                    x.ProductId,
                    x.Sku,
                    x.ProductName,
                    x.Quantity,
                    x.UnitPrice))
                .ToList());
    }

    public static OrderResponse ToResponse(this OrderDto order)
    {
        return new OrderResponse(
            Id: order.Id,
            CreatedAtUtc: order.CreatedAtUtc,
            Status: order.Status,
            TotalAmount: order.TotalAmount,
            Items: order.Items
                .Select(x => new OrderItemResponse(
                    Id: x.Id,
                    ProductId: x.ProductId,
                    Sku: x.Sku,
                    ProductName: x.ProductName,
                    Quantity: x.Quantity,
                    UnitPrice: x.UnitPrice,
                    TotalPrice: x.TotalPrice))
                .ToList());
    }

    public static IReadOnlyList<OrderListItemResponse> ToResponse(this IReadOnlyList<OrderListItemDto> orders)
    {
        return orders
            .Select(x => new OrderListItemResponse(
                Id: x.Id,
                CreatedAtUtc: x.CreatedAtUtc,
                Status: x.Status,
                ItemsCount: x.ItemsCount,
                TotalAmount: x.TotalAmount))
            .ToList();
    }
}