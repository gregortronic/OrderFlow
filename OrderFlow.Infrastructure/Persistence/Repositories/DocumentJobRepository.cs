using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Infrastructure.Persistence.Repositories;

public sealed class DocumentJobRepository(OrderFlowDbContext dbContext) : IDocumentJobRepository
{
    public Task AddAsync(DocumentJob documentJob, CancellationToken cancellationToken = default)
    {
        return dbContext.DocumentJobs.AddAsync(documentJob, cancellationToken).AsTask();
    }

    public Task<DocumentJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.DocumentJobs
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DocumentJob>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.DocumentJobs
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}