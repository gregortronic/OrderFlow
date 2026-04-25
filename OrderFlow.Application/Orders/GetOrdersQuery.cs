using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Application.Orders;

public sealed record GetOrdersQuery(
    OrderStatus? Status,
    PageRequest PageInfo,
    SortDirection SortDirection = SortDirection.Desc);