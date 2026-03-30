namespace OrderFlow.Domain.Documents;

public enum DocumentJobStatus
{
    Uploaded = 1,
    Queued = 2,
    Processing = 3,
    Completed = 4,
    Failed = 5,
    Cancelled = 6
}