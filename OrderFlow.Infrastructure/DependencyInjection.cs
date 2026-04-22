using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Infrastructure.Persistence;
using OrderFlow.Infrastructure.Persistence.Repositories;

namespace OrderFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OrderFlowDb")
                               ?? throw new InvalidOperationException("ConnectionStrings:OrderFlowDb is not configured.");

        services.AddDbContext<OrderFlowDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IDocumentJobRepository, DocumentJobRepository>();

        return services;
    }
}