using ECommerce.Application.Auth;
using ECommerce.Application.Features.Users.Commands.RegisterUser;
using ECommerce.Infrastructure.Auth;
using ECommerce.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork uow, IPasswordHasher passwordHasher, AuthTokenIssuer issuer) : IRequestHandler<LoginCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await uow.User.FindByEmailAsync(request.Email.Trim().ToLowerInvariant());
            if (user is null || !passwordHasher.VerifyPassword(user, request.Password))
            {
                throw new UnauthorizedAccessException("Eposta veya şifre hatalı");
            }

            var refreshPlain = await issuer.AddRefreshTokenAsync(user);
            await uow.SaveChangesAsync();
            return issuer.CreateResponse(user, refreshPlain);
        }
    }
}
