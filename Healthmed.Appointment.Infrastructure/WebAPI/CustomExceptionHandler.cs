using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Healthmed.Appointment.Infrastructure.WebAPI
{
    public sealed class CustomExceptionHandler
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = context.Response.StatusCode;

            if (exception is ApplicationException)
            {
                statusCode = StatusCodes.Status422UnprocessableEntity;
            }

            var problem = new ProblemDetails()
            {
                Type = "https://httpstatuses.com/" + statusCode,
                Title = ((HttpStatusCode)statusCode).ToString(),
                Status = statusCode,
                Detail = exception.Message ?? "An error occurred",
                Instance = context.Request.Path
            };

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(json);
        }
    }
}
