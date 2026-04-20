using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Domain.Orders;

namespace OrderFlow.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository(OrderFlowDbContext dbContext) : IOrderRepository
{
    public Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        return dbContext.Orders.AddAsync(order, cancellationToken).AsTask();
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}