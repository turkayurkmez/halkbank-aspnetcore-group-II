using ECommerce.API.Demo;
using ECommerce.API.Middlewares;
using ECommerce.API.Options;
using ECommerce.API.Services;
using ECommerce.API.Store;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//Demo: Scoped bir instance' middleware'a gönderirken sadece InvokeAsync kullanılmalı
//builder.Services.AddScoped<DemoScope>();

builder.Services.Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SECTION_NAME));
builder.Services.AddSingleton<InMemoryStore>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build(); //burada kestrel ve tüm gereksindiğim nesnelerin konfigürasyonu tamamlandı. 
//uygulama endpoint'e gitmeden önce:
//1. Authentication
//2. Authorization
//3. Loglama
//4. Hata yakalama

//app.Use() kullanmak yerine middleware'leri kendi sınıflarına aktardık.Middlewares/

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMidleware>();
app.UseMiddleware<RequestTimingMiddleware>();


var value = app.Configuration["Deger"];
var maxOrderItems = app.Configuration["Shop:MaxOrderItems"];

app.Logger.LogInformation($"dosyadaki değer: {value}");


//app.UseExceptionHandler();

app.MapGet("/ping", () => Results.Ok(new { status = "ok" }));

app.MapControllers();


app.Run();
