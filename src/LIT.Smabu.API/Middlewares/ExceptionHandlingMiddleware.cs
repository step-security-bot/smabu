using LIT.Smabu.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Response response = new();
        context.Response.ContentType = "application/json";

        if (exception is DomainError domainException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            response.StatusCode = context.Response.StatusCode;
            response.Message = domainException.Message;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusCode = context.Response.StatusCode;
            response.Message = exception.Message;
        }

        return context.Response.WriteAsync(response.Message);
    }

    internal class Response
    {
        public int StatusCode { get; internal set; }
        public string? Message { get; internal set; }
    }
}
