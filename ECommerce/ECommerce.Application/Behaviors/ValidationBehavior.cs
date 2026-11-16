using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Behaviors
{

    /*
     * MediatR'ın handle objesine istek gelmeden ÖNCE çalışmasını istediğiniz bir işlem varsa
     * bu işlemleri Behavior nesneleriyle yönetirsiniz. Tıpkı middleware'e benzer ama operasyonu MediatR kütüphanesi yönetir.
     * 
     * Örneğin, şu anda ProductsController içindeki POST isteğinde validasyon yapıyoruz. Validasyonu orada değil de mediatR'ın handle'ına
     * gitmeden önce yapmak istiyoruz:
     */
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = results.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

            if (failures.Count  != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
