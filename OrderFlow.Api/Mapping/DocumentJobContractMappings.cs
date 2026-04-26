using OrderFlow.Api.Contracts.Common;
using OrderFlow.Api.Contracts.Documents;
using OrderFlow.Application.Common.Pagination;
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

    public static GetDocumentJobsQuery ToQuery(this GetDocumentJobsRequest request)
    {
        return new GetDocumentJobsQuery(
            Status: request.Status,
            PageInfo: new PageRequest(request.Page, request.PageSize),
            SortDirection: request.SortDirection);
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

    public static PagedResponse<DocumentJobListItemResponse> ToResponse(
        this PagedResult<DocumentJobListItemDto> documentJobs)
    {
        return new PagedResponse<DocumentJobListItemResponse>(
            Items: documentJobs.Items
                .Select(x => new DocumentJobListItemResponse(
                    Id: x.Id,
                    OriginalFileName: x.OriginalFileName,
                    ContentType: x.ContentType,
                    Status: x.Status,
                    CreatedAtUtc: x.CreatedAtUtc,
                    FinishedAtUtc: x.FinishedAtUtc))
                .ToList(),
            Page: documentJobs.Page,
            PageSize: documentJobs.PageSize,
            TotalCount: documentJobs.TotalCount,
            TotalPages: documentJobs.TotalPages,
            HasPreviousPage: documentJobs.HasPreviousPage,
            HasNextPage: documentJobs.HasNextPage);
    }
}
