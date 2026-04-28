using OrderFlow.Application.Common.Pagination;

namespace OrderFlow.Application.Documents;

public interface IDocumentJobApplicationService
{
    Task<DocumentJobDto> RegisterAsync(CreateDocumentJobCommand command, CancellationToken cancellationToken = default);

    Task<DocumentJobDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DocumentJobDto> StartProcessingAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DocumentJobDto> CompleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DocumentJobDto> FailAsync(Guid id, string errorMessage, CancellationToken cancellationToken = default);

    Task<DocumentJobDto> CancelAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<DocumentJobListItemDto>> GetListAsync(
        GetDocumentJobsQuery query,
        CancellationToken cancellationToken = default);
}