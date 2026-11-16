using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password, string FullName, DateOnly DateOfBirth) : IRequest<AuthResponse>;


    public record AuthResponse(string AcceesToken, string RefreshToken, DateTime ExpiresAt);
   

   
}
