using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using PrintzPh.Domain.Exceptions;
using FluentValidationValidationException = FluentValidation.ValidationException;

namespace PrintzPh.API.Middleware;

public class ExceptionHandlingMiddleware
{
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
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";
    var response = new ErrorResponse();
    switch (exception)
    {
      case FluentValidationValidationException validationEx:
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.Message = "Validation failed";
        response.Errors = validationEx.Errors
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
            .ToList();
        break;

      case UserNotFoundException:
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        response.StatusCode = (int)HttpStatusCode.NotFound;
        response.Message = exception.Message;
        break;

      case DuplicateEmailException:
        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
        response.StatusCode = (int)HttpStatusCode.Conflict;
        response.Message = exception.Message;
        break;

      default:
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.Message = "An unexpected error occurred";
        _logger.LogError(exception, "Unhandled exception occurred");
        break;
    }

    var jsonResponse = JsonSerializer.Serialize(response);
    await context.Response.WriteAsync(jsonResponse);
  }

  private class ErrorResponse
  {
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
  }
}
