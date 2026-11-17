using ECommerce.Application.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Auth;
using ECommerce.Infrastructure.Repositories;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterCommandHandler(IUnitOfWork uow, IPasswordHasher passwordHasher, AuthTokenIssuer issuer) : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var role = await uow.User.FindRoleAsync(Role.Customer) ?? new Role { Name = Role.Customer };

            var user = request.Adapt<User>();

            user.Roles = [role];

            user.Email = request.Email.Trim().ToLowerInvariant();

            user.PasswordHash = passwordHasher.Hash(user, request.Password);

            await uow.User.CreateAsync(user);

            // TODO 1.1: DİKKAT!!! Burada, kullanıcı kaydı yapılırken Refresh token da üretilecek...


            var refreshPlain = await issuer.AddRefreshTokenAsync(user);
            await uow.SaveChangesAsync();
            return issuer.CreateResponse(user, refreshPlain);
        }
    }
}
