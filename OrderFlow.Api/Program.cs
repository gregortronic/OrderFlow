using Microsoft.Extensions.Options;
using OrderFlow.Api.Configuration;
using OrderFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ApplicationOptions>(
    builder.Configuration.GetSection(ApplicationOptions.SectionName));

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var options = scope.ServiceProvider
        .GetRequiredService<IOptions<ApplicationOptions>>()
        .Value;

    if (string.IsNullOrWhiteSpace(options.Name))
    {
        throw new InvalidOperationException("Application:Name is not configured.");
    }
}

app.MapControllers();

app.Run();