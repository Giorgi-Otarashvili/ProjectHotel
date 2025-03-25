using Hotel.Services.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace ProjectHotel.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine($"Exception caught: {exception.GetType().Name} - {exception.Message}");

            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,  // 404
                ValidationException => (int)HttpStatusCode.BadRequest, // 400
                RatingValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError           // 500
            };

            var response = new
            {
                StatusCode = statusCode,
                Message = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
