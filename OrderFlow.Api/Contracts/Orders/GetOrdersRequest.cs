using System.ComponentModel.DataAnnotations;
using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Api.Contracts.Orders;

public sealed class GetOrdersRequest
{
    public OrderStatus? Status { get; init; }

    [Range(1, int.MaxValue)]
    public int Page { get; init; } = PageRequest.DefaultPage;

    [Range(1, PageRequest.MaxPageSize)]
    public int PageSize { get; init; } = PageRequest.DefaultPageSize;

    public SortDirection SortDirection { get; init; } = SortDirection.Desc;
}