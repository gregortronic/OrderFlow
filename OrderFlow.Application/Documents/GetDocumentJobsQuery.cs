using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Common.Sorting;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Application.Documents;

public sealed record GetDocumentJobsQuery(
    DocumentJobStatus? Status,
    PageRequest PageInfo,
    SortDirection SortDirection = SortDirection.Desc);