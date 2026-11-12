using ECommerce.API.Mappings;
using ECommerce.API.Middlewares;
using ECommerce.API.Notifications;
using ECommerce.API.Options;
using ECommerce.API.Services;
using ECommerce.API.Validators;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//Demo: Scoped bir instance' middleware'a gönderirken sadece InvokeAsync kullanılmalı
//builder.Services.AddScoped<DemoScope>();

builder.Services.Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SECTION_NAME));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<INotificationChannel, EmailChannel>();
builder.Services.AddSingleton<INotificationChannel, SMSChannel>();
builder.Services.AddSingleton<INotificationChannel, WhatsAppChannel>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var conectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(conectionString));

//Her yeni ...Validator'u yazmaktansa, bir kere class'ın bulunduğu yeri belirtin geri kalanını otomatik eklesin:
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

builder.Services.AddOpenApi();

TypeAdapterConfig.GlobalSettings.Default.RequireDestinationMemberSource(true);
TypeAdapterConfig.GlobalSettings.Scan(typeof(ProductMappingConfig).Assembly);
builder.Services.AddMapster();
TypeAdapterConfig.GlobalSettings.Compile();

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

    using var scope = app.Services.CreateScope();

    await DbSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<AppDbContext>());


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
