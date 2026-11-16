using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Middlewares
{
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
            catch(FluentValidation.ValidationException ex)
            {
                var response = ex.Errors.GroupBy(e => e.PropertyName)
                                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(new ValidationProblemDetails(response)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bir veya daha fazla doğrulama hatası oluştu!!!"

                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İşlenemeyen hata: {Method}{Path}", context.Request.Method, context.Request.Path);

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Beklenmeyen bir hata oluştu!" });
            }
        }
    }
}
