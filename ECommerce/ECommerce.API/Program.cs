using ECommerce.API.Demo;
using ECommerce.API.Middlewares;
using ECommerce.API.Notifications;
using ECommerce.API.Options;
using ECommerce.API.Repositories;
using ECommerce.API.Services;
using ECommerce.API.Store;
using Scalar.AspNetCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//Demo: Scoped bir instance' middleware'a gönderirken sadece InvokeAsync kullanılmalı
//builder.Services.AddScoped<DemoScope>();

builder.Services.Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SECTION_NAME));
builder.Services.AddSingleton<InMemoryStore>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddSingleton<INotificationChannel, EmailChannel>();
builder.Services.AddSingleton<INotificationChannel, SMSChannel>();
builder.Services.AddSingleton<INotificationChannel, WhatsAppChannel>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();


builder.Services.AddOpenApi();

var app = builder.Build();
//burada kestrel ve tüm gereksindiğim nesnelerin konfigürasyonu tamamlandı. 
//uygulama endpoint'e gitmeden önce:
//1. Authentication
//2. Authorization
//3. Loglama
//4. Hata yakalama

//app.Use() kullanmak yerine middleware'leri kendi sınıflarına aktardık.Middlewares

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

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
