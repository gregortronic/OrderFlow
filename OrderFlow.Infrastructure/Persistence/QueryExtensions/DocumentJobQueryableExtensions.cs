using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Infrastructure.Persistence.QueryExtensions;

public static class DocumentJobQueryableExtensions
{
    public static IOrderedQueryable<DocumentJob> OrderByCreatedAt(
        this IQueryable<DocumentJob> source,
        SortDirection direction)
    {
        return direction == SortDirection.Asc
            ? source.OrderBy(x => x.CreatedAtUtc).ThenBy(x => x.Id)
            : source.OrderByDescending(x => x.CreatedAtUtc).ThenByDescending(x => x.Id);
    }
}