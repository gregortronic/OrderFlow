using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Documents;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Application.Abstractions.Persistence;

public interface IDocumentJobRepository
{
    Task AddAsync(DocumentJob documentJob, CancellationToken cancellationToken = default);

    Task<DocumentJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<DocumentJobListItemDto>> GetListAsync(
        GetDocumentJobsQuery query,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}