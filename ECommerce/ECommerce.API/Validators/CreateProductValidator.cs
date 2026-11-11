using ECommerce.API.Models.Requests;
using ECommerce.Infrastructure.Repositories;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class CreateProductValidator :AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator(IUnitOfWork uow)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı zorunlu")
                              .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olmalıdır")
                              .MinimumLength(2).WithMessage("ürün adı en az 2 karakter olmalı");

            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Fiyat değeri 0'dan büyük olmalı");
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(0).WithMessage("stok negatif olamaz");


            //1. Eğer eklenmeye çalışılan ürünün kategori id'si db'de yoksa, hata versin.
            RuleFor(p => p.CategoryId)
                .MustAsync(async (id, _) => await uow.Categories.GetbyIdAsync(id) is not null)
                .WithMessage(p => $"id'si {p.CategoryId} olan kategori bulunamadı");

            //2. Sku değeri sadece A-Z arası harf 0-9 arası rakam ve - içerir. 
            //   Sku değerinden iki tane olamaz.

            string pattern = "^[A-Z0-9-]{4,20}$";

            RuleFor(p => p.Sku)
                   .Cascade(CascadeMode.Stop)
                   
                   .NotEmpty().WithMessage("SKU değeri zounludur")
                   .Matches(pattern).WithMessage("SKU yalnızca büyük harf, rakam ve '-' içerebilir. Ayrıca 4-20 karakter aralığında olmalıdır ")
                   .MustAsync(async (sku, _) => !await uow.Products.IsSKUExists(sku))
                   .WithMessage(x => $"Bu sku başka bir ürün tarafından kullanılıyor: {x.Sku}");

                
        }
    }
}
