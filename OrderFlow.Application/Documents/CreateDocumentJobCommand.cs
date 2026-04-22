namespace OrderFlow.Application.Documents;

public sealed record CreateDocumentJobCommand(
    string OriginalFileName,
    string ContentType);