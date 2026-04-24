using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Application.Documents;

public sealed record GetDocumentJobsQuery(
    DocumentJobStatus? Status,
    int Page = 1,
    int PageSize = 20,
    SortDirection SortDirection = SortDirection.Desc);