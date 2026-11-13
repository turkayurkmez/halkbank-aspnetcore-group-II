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


## Gün 6

### İşlenen Konular

- `EFProductRepository` ile `IProductRepository` EF Core üzerinden gerçeklendi ve DI'da `AddScoped` ile kaydedildi (`InMemoryProductRepository` yerine).
- CRUD işlemleri `AppDbContext` üzerinden (`AddAsync`, `FindAsync`, `Remove`, `Update`, `SaveChangesAsync`) yapıldı.
- Okuma sorgularında `AsNoTracking` kullanımı ve `Include` ile ilişkili `Category` yükleme gösterildi.
- `Update` çağrısının Id değerine göre INSERT/UPDATE davranışı incelendi.
- `Select` ile `ProductSummary` projeksiyonu (`GetProductSummaries`) eklendi.


## Gün 7 - 10.11.2026

### İşlenen Konular

- Veri erişim kodu `ECommerce.Infrastructure` class library projesine taşındı (`AppDbContext`, `DbSeeder`, entity'ler, repository'ler, migration'lar); `ECommerce.API` bu projeye referans verir.
- Kullanılmayan `InMemoryStore` kaldırıldı; API katmanı yalnızca servis ve controller sorumluluklarını taşır.
- `IEntity` marker interface'i eklendi; entity'ler bu arayüzü uygular ve generic repository `where T : class, IEntity, new()` kısıtıyla sınırlandırıldı.
- Generic `Repository<T>` (`CreateAsync`, `GetAllAsync(predicate)`, `GetbyIdAsync`, `UpdateAsync`, `DeleteAsync`) yazıldı; okuma sorgularında `AsNoTracking` kullanıldı. `EFProductRepository` bu sınıftan türetilerek ürüne özgü sorgularla genişletildi.
- **Unit of Work** deseni uygulandı: `IUnitOfWork` / `UnitOfWork`, `Products` ve `Categories` repository'lerini tek `AppDbContext` örneği üzerinden sunar (lazy oluşturma, `??=`).
- Repository metotları artık `SaveChanges` çağırmaz; değişiklikler `IUnitOfWork.SaveChangesAsync()` ile tek noktadan ve tek transaction içinde kaydedilir.
- `ProductService`, `IProductRepository` yerine `IUnitOfWork` üzerinden çalışacak şekilde güncellendi; `Program.cs` içinde `IUnitOfWork` DI'a (`AddScoped`) kaydedildi.

### Vurgulanan Noktalar

- **Marker interface:** Üyesi olmayan arayüz, yalnızca generic tipi kısıtlamak için kullanılır.
- **Unit of Work:** Birden fazla repository'deki değişiklikleri tek iş birimi olarak kaydederek tutarlılık sağlar.
- **Katmanlı mimari:** Persistence'ın Infrastructure'a taşınması bağımlılık yönünü netleştirir (API -> Infrastructure).

## Gün 8 - 11.11.2026

### İşlenen Konular

- FluentValidation paketi `ECommerce.API` projesine eklendi ve `Program.cs` içinde yapılandırıldı.
- `CreateProductRequest` modeli (`Models/Requests`) oluşturuldu; ürün oluşturma isteği entity yerine bu modelle alınır.
- `CreateProductValidator` ile kurallar tanımlandı: ürün adı zorunlu (2-200 karakter), fiyat 0'dan büyük, stok negatif olamaz.
- Validator, `IUnitOfWork` enjekte edilerek veritabanı kontrolü yapar: kategori id'si DB'de yoksa hata verir (`MustAsync`).
- `Sku` kuralı: boş olamaz, `^[A-Z0-9-]{4,20}$` desenine uymalı ve benzersiz olmalı; `Cascade(CascadeMode.Stop)` ile ilk hatada durulur.
- `IProductRepository` / `EFProductRepository` içine `IsSKUExists` metodu eklendi.
tek `ValidationProblem` yanıtında döner.
- `ProductListResponse`
- `CategoriesController` ile kategori endpoint'i eklendi.
- `IProductRepository` / `EFProductRepository` içine `ListWithCategoryAsync` ve `GetProductWithCategory` eklendi; `Include` ile `Category` yüklenir, `Include` bilgisi yalnızca EF implementasyonunda kalır.
- **Mapster** projeye eklendi; manuel `Select` ile eşleme yerine `Adapt<T>()` kullanıldı.
- `ProductMappingConfig` (`IRegister`) ile özel eşlemeler yazıldı: `Category.Name` -> `CategoryName`, `CreateProductRequest` -> `Product` (`Id`, `Reviews`, `Category`, `OrderItems` yok sayılır).
- `Program.cs` içinde `RequireDestinationMemberSource(true)`, `Scan` ve `Compile` ile eşleme kuralları başlangıçta taranıp doğrulanır.

### Vurgulanan Noktalar

- **Asenkron doğrulama:** Veritabanı gerektiren kurallar `MustAsync` ile yazılır.
- **Extension method:** Tekrarlanan hata dönüştürme kodu tek noktada toplanır.
- **Cascade mode:** Gereksiz DB sorgusunu önlemek için ilk başarısız kuralda durulur.
- **DTO:** Entity'ler API dışına sızmaz; yanıt şekli servis katmanında DTO ile belirlenir.
farklı olanlar için `IRegister` ile config yazılır.

### Benchmark Sonucu: Manuel Mapping vs Mapster

`Demo.Benchmark` projesinde BenchmarkDotNet ile `Product` -> `ProductListResponse` dönüşümü ölçüldü (.NET 10, Release).

| Method | Count | Mean | Ratio | Allocated |
|---|---|---|---|---|
| Manual | 1 | 76,39 ns | 1,00 | 128 B |
| Mapster | 1 | 156,76 ns | 2,07 | 128 B |
| Manual | 1000 | 50,01 µs | 1,00 | 72 KB |
| Mapster | 1000 | 86,89 µs | 1,75 | 72 KB |

**Çıkarım:** Manuel mapping, Mapster'dan yaklaşık 1,7-2 kat hızlıdır; bellek tahsisi aynıdır. Mutlak fark 1000 kayıtta yaklaşık 37 µs olduğundan, çoğu API senaryosunda Mapster'ın bakım kolaylığı bu farkı karşılar. Çok yüksek hacimli kritik yollarda manuel mapping tercih edilebilir.
## Gün 9 - 13.11.2026

### İşlenen Konular

- Çözüm katmanlı (Clean Architecture) yapıya geçirildi: `ECommerce.Domain`, `ECommerce.Application`, `ECommerce.Infrastructure` ve `ECommerce.API` projeleri oluşturuldu (`ECommerce.slnx` güncellendi).
- Entity'ler (`Product`, `Category`, `Customer`, `Order`, `OrderItem`, `Review`, `IEntity`) `ECommerce.Domain/Entities` altına taşındı.
- Servisler, validator'lar, bildirim kanalları (`EmailChannel`, `SMSChannel`, `WhatsAppChannel`), `ShopOptions`, Mapster config'i ve request/response modelleri API'den `ECommerce.Application` projesine taşındı.
- Her katman kendi DI kaydını yapar: `AddApplication()` (`DepedencyInjection.cs`) ve `AddInfrastructure(connectionString)` (`DependencyInjection.cs`); `Program.cs` yalnızca bu iki extension metodu çağırır ve API projesi artık sadece Application'a referans verir.
- Ürün akışı **MediatR** ile CQRS yapısına taşındı (`Features/Products` altında):
  - Command: `CreateProductCommand` (`IRequest<CreateProductResponse>`) ve `CreateProductHandler` (Mapster ile `Product`'a eşler, `IUnitOfWork` ile kaydeder).
  - Query: `GetProductsQuery` ve `GetProductsHandler` (`GetProductResponse` listesi döner).
- `CreateProductValidator`, `CreateProductRequest` yerine `CreateProductCommand` üzerinde çalışacak şekilde güncellendi.
- `IProductService` içine, servis sınıfının her davranış için yeni metotla şişmemesi gerektiğini anlatan açıklama eklendi.

### Vurgulanan Noktalar

- **Katmanlı mimari:** Bağımlılık yönü API -> Application -> Domain; Infrastructure veri erişimini sağlar.
- **Katman başına DI:** Her proje kendi servislerini `Add...` extension metoduyla kaydeder, `Program.cs` sadeleşir.
- **CQRS / MediatR:** Her özellik bir nesne kombinasyonudur: Request (ne alır) -> Response (ne döner) -> Handler (ne/nasıl yapar).
- **Service şişmesi:** Tek bir servis sınıfına her özellik için metot eklemek yerine özellik başına handler yazılır.
