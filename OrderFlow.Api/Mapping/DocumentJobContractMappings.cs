using OrderFlow.Api.Contracts.Documents;
using OrderFlow.Application.Documents;

namespace OrderFlow.Api.Mapping;

public static class DocumentJobContractMappings
{
    public static CreateDocumentJobCommand ToCommand(this CreateDocumentJobRequest request)
    {
        return new CreateDocumentJobCommand(
            OriginalFileName: request.OriginalFileName,
            ContentType: request.ContentType);
    }

    public static DocumentJobResponse ToResponse(this DocumentJobDto documentJob)
    {
        return new DocumentJobResponse(
            Id: documentJob.Id,
            OriginalFileName: documentJob.OriginalFileName,
            StoredFileName: documentJob.StoredFileName,
            ContentType: documentJob.ContentType,
            Status: documentJob.Status,
            CreatedAtUtc: documentJob.CreatedAtUtc,
            ProcessingStartedAtUtc: documentJob.ProcessingStartedAtUtc,
            FinishedAtUtc: documentJob.FinishedAtUtc,
            ErrorMessage: documentJob.ErrorMessage);
    }

    public static IReadOnlyList<DocumentJobListItemResponse> ToResponse(
        this IReadOnlyList<DocumentJobListItemDto> documentJobs)
    {
        return documentJobs
            .Select(x => new DocumentJobListItemResponse(
                Id: x.Id,
                OriginalFileName: x.OriginalFileName,
                ContentType: x.ContentType,
                Status: x.Status,
                CreatedAtUtc: x.CreatedAtUtc,
                FinishedAtUtc: x.FinishedAtUtc))
            .ToList();
    }
}