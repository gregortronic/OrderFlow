using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Infrastructure.Persistence.QueryExtensions;

internal static class OrderQueryableExtensions
{
    public static IOrderedQueryable<Order> OrderByCreatedAt(
        this IQueryable<Order> source,
        SortDirection direction)
    {
        return direction == SortDirection.Asc
            ? source.OrderBy(x => x.CreatedAtUtc).ThenBy(x => x.Id)
            : source.OrderByDescending(x => x.CreatedAtUtc).ThenByDescending(x => x.Id);
    }
}