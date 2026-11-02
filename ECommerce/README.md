# ECommerce (.NET 10) Öğrenme Günlüğü

Bu repository, şu anda **.NET 10** hedefleyen başlangıç seviyesinde bir ASP.NET Core Web API projesi içermektedir.

## Günlük Notlar

| Gün | Bu repo'da ele alınan konular | Öğrenme çıktıları |
|-----|-------------------------------|-------------------|
| Gün 1 | `WebApplication.CreateBuilder` ile ilk API kurulumu, controller kaydı, minimal endpoint mapping (`/ping`) ve middleware pipeline temelleri (request logging + `OnStarting` ile response timing) | ASP.NET Core API uygulamasının nasıl ayağa kalktığını, middleware sıralamasının request/response akışını nasıl etkilediğini, gelen/giden isteklerin nasıl loglanacağını ve `X-Response-Time-Ms` gibi özel response header bilgilerinin nasıl ekleneceğini anlama. |

## Mevcut Proje Yapısı

- `ECommerce.API/Program.cs`  
  Uygulama başlangıcı, middleware yapılandırması ve endpoint mapping.
- `ECommerce.API/Controllers/SampleController.cs`  
  Temel örnek controller endpoint'i.
- `ECommerce.API/ECommerce.API.csproj`  
  `net10.0` hedefleyen proje tanımı.

## Özet

Bu repo şu an, bir e-commerce API backend'i için **Gün 1 temel çalışmasını** temsil etmektedir: startup configuration, middleware kavramları ve ilk endpoint yayını.
