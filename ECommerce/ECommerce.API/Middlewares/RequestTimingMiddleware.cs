using System.Diagnostics;

namespace ECommerce.API.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;
        // private readonly DemoScope _scope;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {

            //_logger.LogInformation("İstek burada: Timing middleware");

            var stopWatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Response-Time-Ms"] = stopWatch.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });

            await _next(context);

            _logger.LogWarning("Dönen yanıt, {time} ms'de oluştu.", stopWatch.ElapsedMilliseconds.ToString());
            stopWatch.Stop();
            /*
             * Bilgi: HTTP header bilgisi, body'den ayrı gider. Daha net yazayım: Body'den önce gider. Eğer geçen süreyi header'a yazmak istersem, yanıt oluşmadan ÖNCE tetiklenecek bir metoda ihtiyacım var.
             */
        }
    }
}
