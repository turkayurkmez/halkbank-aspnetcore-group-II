using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Email)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Eposta alanı zorunludur")
                  .EmailAddress().WithMessage("Lütfen geçerli eposta girin")
                  .MaximumLength(250).WithMessage("En fazla 250 karaktr olabilir");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı bol olamaz..");


                  
            
        }
    }
}
