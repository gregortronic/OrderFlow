using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Abstractions.Persistence;
using OrderFlow.Application.Common.Pagination;
using OrderFlow.Application.Orders;
using OrderFlow.Domain.Orders;
using OrderFlow.Infrastructure.Persistence.Repositories.QueryExtensions;

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

    public Task<Order?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Orders
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PagedResult<OrderListItemDto>> GetListAsync(
        GetOrdersQuery request,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = dbContext.Orders.AsNoTracking();

        if (request.Status is not null)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var rows = await query
            .OrderByCreatedAt(request.SortDirection)
            .Skip(request.PageInfo.Skip)
            .Take(request.PageInfo.PageSize)
            .Select(x => new OrderListRow(
                x.Id,
                x.CreatedAtUtc,
                x.Status,
                x.Items.Count,
                x.Items.Sum(item => item.Quantity * item.UnitPrice)))
            .ToListAsync(cancellationToken);

        var items = rows
            .Select(x => new OrderListItemDto(
                Id: x.Id,
                CreatedAtUtc: x.CreatedAtUtc,
                Status: x.Status.ToString(),
                ItemsCount: x.ItemsCount,
                TotalAmount: x.TotalAmount))
            .ToList();

        return new PagedResult<OrderListItemDto>(
            Items: items,
            Page: request.PageInfo.Page,
            PageSize: request.PageInfo.PageSize,
            TotalCount: totalCount);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private sealed record OrderListRow(
        Guid Id,
        DateTimeOffset CreatedAtUtc,
        OrderStatus Status,
        int ItemsCount,
        decimal TotalAmount);
}