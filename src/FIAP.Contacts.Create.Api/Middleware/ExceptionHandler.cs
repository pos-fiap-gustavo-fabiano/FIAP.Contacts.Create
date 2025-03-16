using ErrorOr;
using System.Net;
using System.Text.Json;

namespace FIAP.Contacts.Create.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory logger)
    {
        _next = next;
        _logger = logger.CreateLogger<ExceptionHandlerMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        _logger.LogError(exception?.Message);

        Error[] response = [Error.Failure(description: "Não foi possível completar a requisição.")];

        var jsonResponse = JsonSerializer.Serialize(response);
        
        return context.Response.WriteAsync(jsonResponse);
    }
}
