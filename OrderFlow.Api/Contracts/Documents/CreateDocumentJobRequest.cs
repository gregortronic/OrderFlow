using System.ComponentModel.DataAnnotations;
using OrderFlow.Api.Validation;

namespace OrderFlow.Api.Contracts.Documents;

public sealed class CreateDocumentJobRequest
{
    [Required]
    [NotBlank]
    [StringLength(256)]
    public string OriginalFileName { get; init; } = string.Empty;

    [Required]
    [NotBlank]
    [StringLength(128)]
    public string ContentType { get; init; } = string.Empty;
}