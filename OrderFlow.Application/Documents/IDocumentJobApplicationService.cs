using OrderFlow.Application.Common.Pagination;

namespace OrderFlow.Application.Documents;

public interface IDocumentJobApplicationService
{
    Task<DocumentJobDto> RegisterAsync(CreateDocumentJobCommand command, CancellationToken cancellationToken = default);

    Task<DocumentJobDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<DocumentJobListItemDto>> GetListAsync(
        GetDocumentJobsQuery query,
        CancellationToken cancellationToken = default);
}