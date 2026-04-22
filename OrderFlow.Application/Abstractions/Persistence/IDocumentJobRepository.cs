using OrderFlow.Domain.Documents;

namespace OrderFlow.Application.Abstractions.Persistence;

public interface IDocumentJobRepository
{
    Task AddAsync(DocumentJob documentJob, CancellationToken cancellationToken = default);
    Task<DocumentJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DocumentJob>> GetListAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}