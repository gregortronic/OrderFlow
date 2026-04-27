using OrderFlow.Domain.Documents;

namespace OrderFlow.Application.Documents;

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