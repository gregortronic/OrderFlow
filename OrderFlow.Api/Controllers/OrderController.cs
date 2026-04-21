using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Contracts.Orders;
using OrderFlow.Api.Mapping;
using OrderFlow.Application.Orders;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("orders")]
public sealed class OrdersController(IOrderApplicationService orderApplicationService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderResponse>> Create(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var createdOrder = await orderApplicationService.CreateAsync(request.ToCommand(), cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdOrder.Id },
            createdOrder.ToResponse());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderApplicationService.GetByIdAsync(id, cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        return Ok(order.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType<IReadOnlyList<OrderListItemResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OrderListItemResponse>>> GetList(CancellationToken cancellationToken)
    {
        var orders = await orderApplicationService.GetListAsync(cancellationToken);
        return Ok(orders.ToResponse());
    }
}