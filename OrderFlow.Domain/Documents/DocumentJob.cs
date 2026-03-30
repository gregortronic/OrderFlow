using OrderFlow.Domain.Common;

namespace OrderFlow.Domain.Documents;

public sealed class DocumentJob
{
    private DocumentJob()
    {
    }

    public Guid Id { get; private set; }

    public string OriginalFileName { get; private set; } = string.Empty;

    public string StoredFileName { get; private set; } = string.Empty;

    public string ContentType { get; private set; } = string.Empty;

    public DocumentJobStatus Status { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public DateTimeOffset? ProcessingStartedAtUtc { get; private set; }

    public DateTimeOffset? FinishedAtUtc { get; private set; }

    public string? ErrorMessage { get; private set; }

    public static DocumentJob Create(string originalFileName, string storedFileName, string contentType)
    {
        if (string.IsNullOrWhiteSpace(originalFileName))
        {
            throw new DomainException("Original file name is required.");
        }

        if (string.IsNullOrWhiteSpace(storedFileName))
        {
            throw new DomainException("Stored file name is required.");
        }

        if (string.IsNullOrWhiteSpace(contentType))
        {
            throw new DomainException("Content type is required.");
        }

        return new DocumentJob
        {
            Id = Guid.NewGuid(),
            OriginalFileName = originalFileName.Trim(),
            StoredFileName = storedFileName.Trim(),
            ContentType = contentType.Trim(),
            Status = DocumentJobStatus.Uploaded,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }

    public void MarkQueued()
    {
        if (Status != DocumentJobStatus.Uploaded)
        {
            throw new DomainException("Only uploaded document jobs can be queued.");
        }

        Status = DocumentJobStatus.Queued;
    }

    public void StartProcessing()
    {
        if (Status != DocumentJobStatus.Queued)
        {
            throw new DomainException("Only queued document jobs can start processing.");
        }

        Status = DocumentJobStatus.Processing;
        ProcessingStartedAtUtc = DateTimeOffset.UtcNow;
        ErrorMessage = null;
    }

    public void Complete()
    {
        if (Status != DocumentJobStatus.Processing)
        {
            throw new DomainException("Only processing document jobs can be completed.");
        }

        Status = DocumentJobStatus.Completed;
        FinishedAtUtc = DateTimeOffset.UtcNow;
        ErrorMessage = null;
    }

    public void Fail(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new DomainException("Error message is required.");
        }

        if (Status is DocumentJobStatus.Completed or DocumentJobStatus.Cancelled)
        {
            throw new DomainException("Completed or cancelled document jobs cannot be failed.");
        }

        Status = DocumentJobStatus.Failed;
        FinishedAtUtc = DateTimeOffset.UtcNow;
        ErrorMessage = errorMessage.Trim();
    }

    public void Cancel()
    {
        if (Status is DocumentJobStatus.Completed or DocumentJobStatus.Cancelled)
        {
            throw new DomainException("Completed or cancelled document jobs cannot be cancelled.");
        }

        Status = DocumentJobStatus.Cancelled;
        FinishedAtUtc = DateTimeOffset.UtcNow;
    }
}