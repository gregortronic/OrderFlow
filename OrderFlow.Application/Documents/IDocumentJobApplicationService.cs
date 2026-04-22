namespace OrderFlow.Application.Documents;

public interface IDocumentJobApplicationService
{
    Task<DocumentJobDto> RegisterAsync(CreateDocumentJobCommand command, CancellationToken cancellationToken = default);
    Task<DocumentJobDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DocumentJobListItemDto>> GetListAsync(CancellationToken cancellationToken = default);
}