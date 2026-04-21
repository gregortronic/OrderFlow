using System.ComponentModel.DataAnnotations;
using OrderFlow.Api.Validation;

namespace OrderFlow.Api.Contracts.Orders;

public sealed class CreateOrderRequest
{
    [Required]
    [MinLength(1)]
    public List<CreateOrderItemRequest> Items { get; init; } = [];
}

public sealed class CreateOrderItemRequest
{
    [NotEmptyGuid]
    public Guid ProductId { get; init; }

    [NotBlank]
    [StringLength(64)]
    public string Sku { get; init; } = string.Empty;

    [NotBlank]
    [StringLength(256)]
    public string ProductName { get; init; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal UnitPrice { get; init; }
}