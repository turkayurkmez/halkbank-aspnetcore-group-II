using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Mappings;
using ECommerce.Application.Notifications;
using ECommerce.Application.Options;
using ECommerce.Application.Services;
using ECommerce.Application.Validators;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           

            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<INotificationChannel, EmailChannel>();
            services.AddSingleton<INotificationChannel, SMSChannel>();
            services.AddSingleton<INotificationChannel, WhatsAppChannel>();
            services.AddScoped<NotificationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();


            //Her yeni ...Validator'u yazmaktansa, bir kere class'ın bulunduğu yeri belirtin geri kalanını otomatik eklesin:
            //Aşağıdaki konfigürasyonlar varsayılan yaklaşımı ezer. Yani normalde isim-tip eşleşmesi yaparken, siz ProductMappingConfig ile bu davranışı override edersiniz.


            //TypeAdapterConfig.GlobalSettings.Default.RequireDestinationMemberSource(true);
            //TypeAdapterConfig.GlobalSettings.Scan(typeof(ProductMappingConfig).Assembly);
            //services.AddMapster();
            //TypeAdapterConfig.GlobalSettings.Compile();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<CreateProductCommand>();
            });

            return services;
        }
    }
}
