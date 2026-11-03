# ECommerce Eğitim Notları

## Gün 1 - 03.11.2026

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
