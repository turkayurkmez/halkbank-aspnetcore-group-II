using ECommerce.Application.Exceptions;
using FluentValidation;
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
            catch (FluentValidation.ValidationException ex)
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
                    _logger.LogError(ex, "Yanıt gönderilmeye başladıktan sonra hata: {Method} {Path}", context.Request.Method, context.Request.Path);
                    throw;
                }

                await WriteProblemAsync(context, ex);
            }
        }

        async Task WriteProblemAsync(HttpContext context, Exception ex)
        {

            var instance = context.Request.Path.Value;
            var extensions = new Dictionary<string, object>()
            {
                ["traceId"] = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier
            };

            IResult result;
            switch (ex)
            {
                case ValidationException validationException:
                    //örnektir:
                    result = Problem(400, Title(StatusCodes.Status400BadRequest), validationException.Message, null, instance, extensions);
                    break;
                case AppExceptions appExceptions:
                    _logger.LogWarning("{ExceptionType} -> {Status Code}: {Message}", appExceptions.GetType().Name, appExceptions.StatusCode, appExceptions.Message);

                    result = Problem(appExceptions.StatusCode, Title(appExceptions.StatusCode), appExceptions.Message, null, instance, extensions);
                    break;
                case UnauthorizedAccessException unauthorized:
                    result = Problem(StatusCodes.Status401Unauthorized, Title(StatusCodes.Status401Unauthorized), unauthorized.Message, null, instance, extensions);
                    break;

                default:
                    result = Problem(StatusCodes.Status500InternalServerError, "İşlenmeyan hata", ex.Message, null, instance, extensions);
                    break;
            }

            await result.ExecuteAsync(context);


        }

        private string Title(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "Doğrulama Hatası",
                StatusCodes.Status404NotFound => "Sayfa Bulunamadı",
                StatusCodes.Status403Forbidden => "Yetkisiz kullanıcı",
                StatusCodes.Status401Unauthorized => "Tanınmayan kullanıcı"
            };
        }

        private IResult Problem(int statusCode, string title, string message, string? detail, string? instance, Dictionary<string, object> extensions)
        {
            return Results.Problem(detail: detail, instance: instance, statusCode: statusCode, title: title, extensions: extensions);
        }
    }
}
