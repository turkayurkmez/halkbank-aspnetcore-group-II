using ECommerce.API.AuthPolicies;
using ECommerce.API.Middlewares;
using ECommerce.Application;
using ECommerce.Application.Auth;
using ECommerce.Application.Mappings;
using ECommerce.Application.Notifications;
using ECommerce.Application.Options;
using ECommerce.Application.Services;
using ECommerce.Application.Validators;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;



Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services);
});

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//Demo: Scoped bir instance' middleware'a gönderirken sadece InvokeAsync kullanılmalı
//builder.Services.AddScoped<DemoScope>();

builder.Services.Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SECTION_NAME));



var conectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddInfrastructure(conectionString);
builder.Services.AddApplication(builder.Configuration);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<JwtOptions>>((bearer, jwtAcessor) =>
                {

                    bearer.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAcessor.Value.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAcessor.Value.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAcessor.Value.SigninKey)),
                        ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                        ValidateLifetime = true,
                        NameClaimType = "Name",
                        RoleClaimType = "role"

                    };

                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("minAge18", policy =>
    {
        policy.RequireAuthenticatedUser()
              .RequireClaim(JwtRegisteredClaimNames.Birthdate)
              .AddRequirements(new MinAgeRequirement(18));
    });
});




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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
