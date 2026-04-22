namespace OrderFlow.Application.Documents;

public sealed record DocumentJobDto(
    Guid Id,
    string OriginalFileName,
    string StoredFileName,
    string ContentType,
    string Status,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? ProcessingStartedAtUtc,
    DateTimeOffset? FinishedAtUtc,
    string? ErrorMessage);

public sealed record DocumentJobListItemDto(
    Guid Id,
    string OriginalFileName,
    string ContentType,
    string Status,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? FinishedAtUtc);