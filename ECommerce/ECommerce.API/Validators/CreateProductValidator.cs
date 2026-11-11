using ECommerce.API.Models.Requests;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class CreateProductValidator :AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı zorunlu")
                              .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olmalıdır")
                              .MinimumLength(2).WithMessage("ürün adı en az 2 karakter olmalı");

            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Fiyat değeri 0'dan büyük olmalı");
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(0).WithMessage("stok negatif olamaz");



        }
    }
}
