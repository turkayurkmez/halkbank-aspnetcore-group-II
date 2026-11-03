# ECommerce Eğitim Notları

## Gün 1 - 02.11.2026

### İşlenen Konular

- .NET 10 hedefli ASP.NET Core Web API projesinin ilk kurulumu yapıldı.
- `ECommerce.slnx` ve API proje yapısı oluşturuldu.
- OpenAPI desteği için gerekli paket referansı eklendi.
- `Program.cs` ile minimal başlangıç (startup) akışı kuruldu.
- `appsettings.json` ve `appsettings.Development.json` ile temel yapılandırma dosyaları eklendi.
- `launchSettings.json` ile geliştirme profilleri ve yerel çalıştırma ayarları tanımlandı.
- MVC controller yapısına geçiş yapıldı ve örnek controller eklendi.
- İstek loglama ve yanıt süresi ölçümü için ilk middleware yaklaşımı uygulandı.
- `X-Response-Time-Ms` header'ı ile yanıt sürelerinin istemciye taşınması sağlandı.

## Gün 2 - 03.11.2026

### İşlenen Konular

- API pipeline'ında özel middleware bileşenleri oluşturuldu ve kaydedildi.
- Merkezi hata yönetimi için exception handling middleware eklendi.
- Gelen HTTP isteklerini izlemek için request logging middleware eklendi.
- İstek süresi ölçümü için request timing middleware eklendi ve `X-Response-Time-Ms` response header'ı üretildi.
- Middleware davranışını doğrulamak için geliştirme ortamına özel örnek endpoint eklendi.
- `Program.cs` içinde bulunan inline middleware yapısı, ayrı middleware sınıflarına taşınarak refactor edildi.
- `appsettings.json`, `appsettings.Development.json` ve `appsettings.Production.json` ile ortama duyarlı yapılandırma uygulandı.
- `ShopOptions` ile strongly typed options tanımlandı ve yapılandırma değerleri controller tarafında bind edildi.
- Launch settings ve proje ayarları, yerel geliştirmede environment bazlı davranışı destekleyecek şekilde güncellendi.
