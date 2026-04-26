using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Contracts.Common;
using OrderFlow.Api.Contracts.Documents;
using OrderFlow.Api.Mapping;
using OrderFlow.Application.Documents;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("documents")]
public sealed class DocumentsController(IDocumentJobApplicationService documentJobApplicationService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<DocumentJobResponse>(StatusCodes.Status202Accepted)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DocumentJobResponse>> Register(
        [FromBody] CreateDocumentJobRequest request,
        CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.RegisterAsync(request.ToCommand(), cancellationToken);

        return AcceptedAtAction(
            nameof(GetById),
            new { id = documentJob.Id },
            documentJob.ToResponse());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<DocumentJobResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DocumentJobResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.GetByIdAsync(id, cancellationToken);
        if (documentJob is null)
        {
            return NotFound();
        }

        return Ok(documentJob.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType<PagedResponse<DocumentJobListItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<DocumentJobListItemResponse>>> GetList(
        [FromQuery] GetDocumentJobsRequest request,
        CancellationToken cancellationToken)
    {
        var documentJobs = 
            await documentJobApplicationService.GetListAsync(request.ToQuery(), cancellationToken);
        return Ok(documentJobs.ToResponse());
    }
}
