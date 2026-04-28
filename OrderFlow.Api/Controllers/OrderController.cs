using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Contracts.Common;
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
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderApplicationService.GetByIdAsync(id, cancellationToken);

        return Ok(order.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType<PagedResponse<OrderListItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<OrderListItemResponse>>> GetList(
        [FromQuery] GetOrdersRequest request,
        CancellationToken cancellationToken)
    {
        var orders = await orderApplicationService.GetListAsync(request.ToQuery(), cancellationToken);

        return Ok(orders.ToResponse());
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<OrderResponse>> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderApplicationService.CancelAsync(id, cancellationToken);

        return Ok(order.ToResponse());
    }
}