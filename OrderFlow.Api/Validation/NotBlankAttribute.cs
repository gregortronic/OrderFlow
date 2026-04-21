using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Api.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class NotBlankAttribute : ValidationAttribute
{
    public NotBlankAttribute()
    {
        ErrorMessage = "The field {0} must not be blank.";
    }

    public override bool IsValid(object? value)
    {
        return value is string text && !string.IsNullOrWhiteSpace(text);
    }
}