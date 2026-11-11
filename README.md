# ECommerce

ASP.NET Core (.NET) Web API tabanlı e-ticaret eğitim projesi.

## Proje Yapısı

- `ECommerce.API`: Web API katmanı (Program.cs, request modelleri, FluentValidation validator'ları)
- `ECommerce.Infrastructure`: `AppDbContext`, repository'ler ve `UnitOfWork`

## Gereksinimler

- .NET SDK (projede kullanılan sürüm)
- SQL Server LocalDB

## Kurulum

1. `appsettings.json` içindeki `ConnectionStrings:Default` değerini kontrol edin:
   `Data Source=(localdb)\mssqllocaldb;Initial Catalog=HalkECommerceTraining2;Integrated Security=True`
2. Veritabanını oluşturun (EF Core migration kullanılıyorsa): `dotnet ef database update`
3. Çalıştırın: `dotnet run --project ECommerce.API`

## Dependency Injection Notları

- Servisler arayüzleri üzerinden kaydedilir (örn. `IUnitOfWork` -> `UnitOfWork`).
- Sınıflar constructor'da somut sınıf yerine arayüzü istemelidir
  (örn. `CreateProductValidator(IUnitOfWork uow)`). Aksi halde uygulama açılışında
  "Unable to resolve service for type 'UnitOfWork'" hatası alınır.
- Validator'lar `AddValidatorsFromAssemblyContaining<CreateProductValidator>()` ile otomatik eklenir.

## Doğrulama Kuralları (CreateProductRequest)

- Ad: zorunlu, 2-200 karakter
- Fiyat: 0'dan büyük
- Stok: negatif olamaz
- Kategori: veritabanında mevcut olmalı
- SKU: zorunlu, `^[A-Z0-9-]{4,20}$` desenine uymalı ve benzersiz olmalı
