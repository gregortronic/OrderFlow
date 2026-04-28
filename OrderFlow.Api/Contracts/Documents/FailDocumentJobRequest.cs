using System.ComponentModel.DataAnnotations;
using OrderFlow.Api.Validation;

namespace OrderFlow.Api.Contracts.Documents;

public sealed class FailDocumentJobRequest
{
    [NotBlank]
    [StringLength(2000)]
    public string ErrorMessage { get; init; } = string.Empty;
}