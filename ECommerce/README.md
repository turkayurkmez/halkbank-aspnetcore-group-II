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

## Gün 3 - 04.11.2026

### İşlenen Konular

- Sipariş oluşturma akışı için `OrderController` ve `OrderService` katmanları eklendi.
- Bildirim gönderimi için `INotificationChannel` sözleşmesi tanımlandı.
- `EmailChannel`, `SMSChannel` ve `WhatsAppChannel` ile çoklu bildirim kanalı desteği geliştirildi.
- `NotificationService`, tüm bildirim kanallarını tek noktadan çalıştıracak şekilde tasarlandı.
- Bildirim altyapısı dependency injection ile `Program.cs` üzerinden kayıt edilerek genişletilebilir hale getirildi.

### SOLID Vurgusu (SRP / OCP)

- **SRP:** `OrderController` sadece HTTP istek/yanıt sorumluluğunu taşır; iş kuralı `OrderService` içinde tutulur.
- **SRP:** Her kanal sınıfı (`EmailChannel`, `SMSChannel`, `WhatsAppChannel`) yalnızca kendi gönderim davranışından sorumludur.
- **OCP:** `NotificationService`, `INotificationChannel` üzerinden çalıştığı için yeni bir kanal eklemek mevcut servisi değiştirmeden mümkündür.
- **OCP:** `Program.cs` içinde yeni bildirim kanalları yalnızca DI kaydı eklenerek sisteme dahil edilebilir.

## Gün 4 - 05.11.2026

### İşlenen Konular

- Ürün ve sipariş akışları için servis soyutlamaları (`IProductService`, `IOrderService`) tanımlandı.
- Veri erişim katmanında generic repository sözleşmesi (`IRepository<T>`) ve ürün odaklı sözleşme (`IProductRepository`) eklendi.
- `InMemoryProductRepository`, `IProductRepository` üzerinden DI ile sisteme bağlandı.
- `ProductsController` ve `OrderController`, somut sınıflar yerine servis arayüzlerine bağımlı olacak şekilde güncellendi.
- `Program.cs` içinde servis ve repository kayıtları arayüz-temelli bağımlılık yönetimine göre düzenlendi.
- `Demo.SOLID` projesinde matematik örnekleri `IMath` ve `IAdvMath` arayüzlerine ayrılarak istemciye ihtiyacı kadar sözleşme sunan bir yapı gösterildi.

### SOLID Vurgusu (DIP / ISP)

- **DIP:** Controller katmanı (`ProductsController`, `OrderController`) doğrudan somut servis sınıflarına değil, `IProductService` ve `IOrderService` soyutlamalarına bağlandı.
- **DIP:** `ProductService`, veri kaynağını doğrudan yönetmek yerine `IProductRepository` üzerinden çalışacak şekilde kurgulandı; böylece altyapı detayları üst katmandan ayrıştırıldı.
- **DIP:** Uygulama başlangıcında (`Program.cs`) bağımlılıklar DI container üzerinden çözülerek üst seviye modüllerin alt seviye implementasyonlara sıkı bağlılığı azaltıldı.
- **ISP:** `IRepository<T>` temel CRUD sorumluluklarını taşırken, ürüne özgü davranışlar (`SearchByNameAsync`) ayrı bir arayüzde (`IProductRepository`) tutuldu.
- **ISP:** `IMath` ve `IAdvMath` ayrımı ile temel hesaplama ihtiyacı olan sınıfların gelişmiş matematik operasyonlarını zorunlu olarak implemente etmesinin önüne geçildi.

## Gün 5 - 06.11.2026

### İşlenen Konular

- DI yaşam döngüleri (Singleton / Scoped / Transient) için `DEMO.DILifeCycle` MVC projesi ile POC yapıldı.
- `ISingleton`, `IScoped`, `ITransient` ve `GuidService` ile aynı istek içinde ve istekler arasında örnek paylaşımı gözlemlendi.
- `ECommerce.API` projesine EF Core (SQL Server) eklendi; bağlantı bilgisi `appsettings.json` içinde tanımlandı.
- `Product`, `Category`, `Review`, `Customer`, `Order` ve `OrderItem` entity modelleri oluşturuldu.
- `AppDbContext` içinde Fluent API ile kısıtlar tanımlandı (zorunlu alanlar, `MaxLength`, `SKU` için unique index, `Category.Name` index'i).
- Product-Category ilişkisi `DeleteBehavior.Restrict` ile yapılandırıldı.
- `initialize` ve `indexes` migration'ları oluşturuldu.
- `DbSeeder` ile başlangıç verisi eklendi ve `Program.cs` içinde DbContext kaydı yapıldı.

