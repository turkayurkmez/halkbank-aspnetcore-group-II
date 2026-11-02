using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build(); //burada kestrel ve tüm gereksindiğim nesnelerin konfigürasyonu tamamlandı. 
//uygulama endpoint'e gitmeden önce:
//1. Authentication
//2. Authorization
//3. Loglama
//4. Hata yakalama

app.Use(async (context, next) =>
{
    //next ÇAĞRILMADAN ÖNCE: istek henüz içeri geliyor.
    app.Logger.LogInformation("İstek burada: Logging middleware");
    await next(context);
    //next'den SONRA: yanıt üretildi ve geri döndü. Örneğin status code artık belli
    app.Logger.LogInformation("{Method} isteği, [{Path}] adresine geldi -> {StatusCode} döndürdü", context.Request.Method, context.Request.Path, context.Response.StatusCode);
});

app.Use(async (context, next) =>
{
    app.Logger.LogInformation("İstek burada: Timing middleware");

    var stopWatch = Stopwatch.StartNew();

    context.Response.OnStarting(() =>
    {
        context.Response.Headers["X-Response-Time-Ms"] = stopWatch.ElapsedMilliseconds.ToString();
        return Task.CompletedTask;
    });

    await next(context);

    app.Logger.LogWarning("Dönen yanıt, {time} ms'de oluştu.", stopWatch.ElapsedMilliseconds.ToString());
    stopWatch.Stop();
    /*
     * Bilgi: HTTP header bilgisi, body'den ayrı gider. Daha net yazayım: Body'den önce gider. Eğer geçen süreyi header'a yazmak istersem, yanıt oluşmadan ÖNCE tetiklenecek bir metoda ihtiyacım var.
     */
    
   
});

app.MapGet("/ping", () => Results.Ok(new { status = "ok" }));

app.MapControllers();


app.Run();
