﻿using OrderFlow.Application.Abstractions.Persistence;
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

        await documentJobRepository.AddAsync(documentJob, cancellationToken);
        await documentJobRepository.SaveChangesAsync(cancellationToken);

        return documentJob.ToDto();
    }

    public async Task<DocumentJobDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var documentJob = await documentJobRepository.GetByIdAsync(id, cancellationToken);
        return documentJob?.ToDto();
    }

    public async Task<PagedResult<DocumentJobListItemDto>> GetListAsync(
        GetDocumentJobsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await documentJobRepository.GetListAsync(query, cancellationToken);
    }


    private static string GenerateStoredFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        return $"{Guid.NewGuid():N}{extension}";
    }
}

internal static class DocumentJobMappings
{
    public static DocumentJobDto ToDto(this DocumentJob documentJob)
    {
        return new DocumentJobDto(
            Id: documentJob.Id,
            OriginalFileName: documentJob.OriginalFileName,
            StoredFileName: documentJob.StoredFileName,
            ContentType: documentJob.ContentType,
            Status: documentJob.Status.ToString(),
            CreatedAtUtc: documentJob.CreatedAtUtc,
            ProcessingStartedAtUtc: documentJob.ProcessingStartedAtUtc,
            FinishedAtUtc: documentJob.FinishedAtUtc,
            ErrorMessage: documentJob.ErrorMessage);
    }

    public static DocumentJobListItemDto ToListItemDto(this DocumentJob documentJob)
    {
        return new DocumentJobListItemDto(
            Id: documentJob.Id,
            OriginalFileName: documentJob.OriginalFileName,
            ContentType: documentJob.ContentType,
            Status: documentJob.Status.ToString(),
            CreatedAtUtc: documentJob.CreatedAtUtc,
            FinishedAtUtc: documentJob.FinishedAtUtc);
    }
}