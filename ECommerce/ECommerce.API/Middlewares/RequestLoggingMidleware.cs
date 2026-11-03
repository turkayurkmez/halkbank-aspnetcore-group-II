namespace ECommerce.API.Middlewares
{
    public class RequestLoggingMidleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMidleware> _logger;

        public RequestLoggingMidleware(RequestDelegate next, ILogger<RequestLoggingMidleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //next ÇAĞRILMADAN ÖNCE: istek henüz içeri geliyor.
            //_logger.LogInformation("İstek burada: Logging middleware");
            await _next(context);
            //next'den SONRA: yanıt üretildi ve geri döndü. Örneğin status code artık belli
            _logger.LogInformation("{Method} isteği, [{Path}] adresine geldi -> {StatusCode} döndürdü", context.Request.Method, context.Request.Path, context.Response.StatusCode);
        }
    }
}
