using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Documents;
using OrderFlow.Domain.Inventory;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Infrastructure.Persistence;

public sealed class OrderFlowDbContext(DbContextOptions<OrderFlowDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<DocumentJob> DocumentJobs => Set<DocumentJob>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderFlowDbContext).Assembly);
    }
}