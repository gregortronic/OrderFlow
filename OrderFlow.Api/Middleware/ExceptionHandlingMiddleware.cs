using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.Common.Exceptions;
using OrderFlow.Domain.Common;

namespace OrderFlow.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private const string ProblemJsonContentType = "application/problem+json";

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogError(
                    exception,
                    "An exception occurred after the response had already started.");

                throw;
            }

            await WriteProblemDetailsAsync(context, exception);
        }
    }

    private async Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
    {
        var error = MapException(exception);

        var problemDetails = new ProblemDetails
        {
            Status = error.StatusCode,
            Title = error.Title,
            Detail = error.StatusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred."
                : exception.Message,
            Type = $"https://httpstatuses.com/{error.StatusCode}",
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        problemDetails.Extensions["errorCode"] = error.ErrorCode;

        context.Response.StatusCode = error.StatusCode;
        context.Response.ContentType = ProblemJsonContentType;

        _logger.Log(
            error.LogLevel,
            exception,
            "Request failed with status code {StatusCode} and error code {ErrorCode}.",
            error.StatusCode,
            error.ErrorCode);

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static ErrorDescriptor MapException(Exception exception)
    {
        return exception switch
        {
            NotFoundException => new ErrorDescriptor(
                StatusCodes.Status404NotFound,
                "Resource not found",
                "RESOURCE_NOT_FOUND",
                LogLevel.Information),

            DomainException => new ErrorDescriptor(
                StatusCodes.Status409Conflict,
                "Business rule violation",
                "BUSINESS_RULE_VIOLATION",
                LogLevel.Warning),

            ArgumentException => new ErrorDescriptor(
                StatusCodes.Status400BadRequest,
                "Invalid request",
                "INVALID_REQUEST",
                LogLevel.Warning),

            _ => new ErrorDescriptor(
                StatusCodes.Status500InternalServerError,
                "Internal server error",
                "INTERNAL_SERVER_ERROR",
                LogLevel.Error)
        };
    }

    private sealed record ErrorDescriptor(
        int StatusCode,
        string Title,
        string ErrorCode,
        LogLevel LogLevel);
}