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
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DocumentJobResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.GetByIdAsync(id, cancellationToken);

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

    [HttpPost("{id:guid}/start-processing")]
    [ProducesResponseType<DocumentJobResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DocumentJobResponse>> StartProcessing(
        Guid id,
        CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.StartProcessingAsync(id, cancellationToken);

        return Ok(documentJob.ToResponse());
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType<DocumentJobResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DocumentJobResponse>> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.CompleteAsync(id, cancellationToken);

        return Ok(documentJob.ToResponse());
    }

    [HttpPost("{id:guid}/fail")]
    [ProducesResponseType<DocumentJobResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DocumentJobResponse>> Fail(
        Guid id,
        [FromBody] FailDocumentJobRequest request,
        CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.FailAsync(
            id,
            request.ErrorMessage,
            cancellationToken);

        return Ok(documentJob.ToResponse());
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType<DocumentJobResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DocumentJobResponse>> Cancel(
        Guid id,
        CancellationToken cancellationToken)
    {
        var documentJob = await documentJobApplicationService.CancelAsync(id, cancellationToken);

        return Ok(documentJob.ToResponse());
    }
}