using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Common.Sorting;
using OrderFlow.Application.Documents;
using OrderFlow.Domain.Documents;
using OrderFlow.Infrastructure.Persistence.QueryExtensions;

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

    public async Task<PagedResult<DocumentJobListItemDto>> GetListAsync(
        GetDocumentJobsQuery request,
        CancellationToken cancellationToken = default)
    {
        IQueryable<DocumentJob> query = dbContext.DocumentJobs.AsNoTracking();

        if (request.Status is not null)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        
        var rows = await query
            .OrderByCreatedAt(request.SortDirection)
            .Skip(request.PageInfo.Skip)
            .Take(request.PageInfo.PageSize)
            .Select(x => new DocumentJobListRow(
                x.Id,
                x.OriginalFileName,
                x.ContentType,
                x.Status,
                x.CreatedAtUtc,
                x.FinishedAtUtc))
            .ToListAsync(cancellationToken);

        var items = rows
            .Select(x => new DocumentJobListItemDto(
                Id: x.Id,
                OriginalFileName: x.OriginalFileName,
                ContentType: x.ContentType,
                Status: x.Status.ToString(),
                CreatedAtUtc: x.CreatedAtUtc,
                FinishedAtUtc: x.FinishedAtUtc))
            .ToList();

        return new PagedResult<DocumentJobListItemDto>(
            Items: items,
            Page: request.PageInfo.Page,
            PageSize: request.PageInfo.PageSize,
            TotalCount: totalCount);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private sealed record DocumentJobListRow(
        Guid Id,
        string OriginalFileName,
        string ContentType,
        DocumentJobStatus Status,
        DateTimeOffset CreatedAtUtc,
        DateTimeOffset? FinishedAtUtc);
}
