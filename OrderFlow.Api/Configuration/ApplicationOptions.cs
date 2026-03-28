namespace OrderFlow.Api.Configuration;

public sealed class ApplicationOptions
{
    public const string SectionName = "Application";

    public string Name { get; set; } = string.Empty;
}