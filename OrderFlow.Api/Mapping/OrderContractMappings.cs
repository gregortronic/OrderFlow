using OrderFlow.Api.Contracts.Common;
using OrderFlow.Api.Contracts.Orders;
using OrderFlow.Application.Common.Pagination;
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

    public static GetOrdersQuery ToQuery(this GetOrdersRequest request)
    {
        return new GetOrdersQuery(
            Status: request.Status,
            PageInfo: new PageRequest(request.Page, request.PageSize),
            SortDirection: request.SortDirection);
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

    public static PagedResponse<OrderListItemResponse> ToResponse(
        this PagedResult<OrderListItemDto> orders)
    {
        return new PagedResponse<OrderListItemResponse>(
            Items: orders.Items
                .Select(x => new OrderListItemResponse(
                    Id: x.Id,
                    CreatedAtUtc: x.CreatedAtUtc,
                    Status: x.Status,
                    ItemsCount: x.ItemsCount,
                    TotalAmount: x.TotalAmount))
                .ToList(),
            Page: orders.Page,
            PageSize: orders.PageSize,
            TotalCount: orders.TotalCount,
            TotalPages: orders.TotalPages,
            HasPreviousPage: orders.HasPreviousPage,
            HasNextPage: orders.HasNextPage);
    }
}
