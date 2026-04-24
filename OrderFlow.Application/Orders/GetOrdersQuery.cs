using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Application.Orders;

public sealed record GetOrdersQuery(
    OrderStatus? Status,
    int Page = 1,
    int PageSize = 20,
    SortDirection SortDirection = SortDirection.Desc);