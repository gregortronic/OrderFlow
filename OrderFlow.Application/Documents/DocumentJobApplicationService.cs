using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Application.Common.Exceptions;
using OrderFlow.Application.Common.Pagination;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Application.Documents;

public sealed class DocumentJobApplicationService(IDocumentJobRepository documentJobRepository)
    : IDocumentJobApplicationService
{
    public async Task<DocumentJobDto> RegisterAsync(
        CreateDocumentJobCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var storedFileName = GenerateStoredFileName(command.OriginalFileName);

        var documentJob = DocumentJob.Create(
            originalFileName: command.OriginalFileName,
            storedFileName: storedFileName,
            contentType: command.ContentType);

        documentJob.MarkQueued();

        await documentJobRepository.AddAsync(documentJob, cancellationToken);
        await documentJobRepository.SaveChangesAsync(cancellationToken);

        return documentJob.ToDto();
    }

    public async Task<DocumentJobDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var documentJob = await documentJobRepository.GetByIdAsync(id, cancellationToken);

        return documentJob is null 
            ? throw new NotFoundException($"Document job '{id}' was not found.") 
            : documentJob.ToDto();
    }

    public async Task<DocumentJobDto> StartProcessingAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var documentJob = await GetRequiredForUpdateAsync(id, cancellationToken);

        documentJob.StartProcessing();

        await documentJobRepository.SaveChangesAsync(cancellationToken);

        return documentJob.ToDto();
    }

    public async Task<DocumentJobDto> CompleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var documentJob = await GetRequiredForUpdateAsync(id, cancellationToken);

        documentJob.Complete();

        await documentJobRepository.SaveChangesAsync(cancellationToken);

        return documentJob.ToDto();
    }

    public async Task<DocumentJobDto> FailAsync(
        Guid id,
        string errorMessage,
        CancellationToken cancellationToken = default)
    {
        var documentJob = await GetRequiredForUpdateAsync(id, cancellationToken);

        documentJob.Fail(errorMessage);

        await documentJobRepository.SaveChangesAsync(cancellationToken);

        return documentJob.ToDto();
    }

    public async Task<DocumentJobDto> CancelAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var documentJob = await GetRequiredForUpdateAsync(id, cancellationToken);

        documentJob.Cancel();

        await documentJobRepository.SaveChangesAsync(cancellationToken);

        return documentJob.ToDto();
    }

    public async Task<PagedResult<DocumentJobListItemDto>> GetListAsync(
        GetDocumentJobsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await documentJobRepository.GetListAsync(query, cancellationToken);
    }

    private async Task<DocumentJob> GetRequiredForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var documentJob = await documentJobRepository.GetByIdForUpdateAsync(id, cancellationToken);

        return documentJob ?? throw new NotFoundException($"Document job '{id}' was not found.");
    }

    private static string GenerateStoredFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        return $"{Guid.NewGuid():N}{extension}";
    }
}