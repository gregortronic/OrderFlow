namespace OrderFlow.Api.Contracts.Documents;

public sealed record DocumentJobResponse(
    Guid Id,
    string OriginalFileName,
    string StoredFileName,
    string ContentType,
    string Status,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? ProcessingStartedAtUtc,
    DateTimeOffset? FinishedAtUtc,
    string? ErrorMessage);

public sealed record DocumentJobListItemResponse(
    Guid Id,
    string OriginalFileName,
    string ContentType,
    string Status,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? FinishedAtUtc);