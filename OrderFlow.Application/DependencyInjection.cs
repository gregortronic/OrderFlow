using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Application.Documents;
using OrderFlow.Application.Orders;

namespace OrderFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderApplicationService, OrderApplicationService>();
        services.AddScoped<IDocumentJobApplicationService, DocumentJobApplicationService>();

        return services;
    }
}