# KOVAN 🐝 – Modüler Kampüs Platformu

> Manisa Celal Bayar Üniversitesi · Yazılım Mühendisliği · **YZM 3215 İleri Web Programlama** dönem projesi
>
> Öğrenciler arı, modüller petek gözü. Her modül ayrı bir iş görür, hepsi ortak altyapıyı (kullanıcı, bildirim, güven puanı, mesajlaşma) paylaşır.

**Teknoloji:** ASP.NET Core 8 MVC (Razor) · Area tabanlı modüler mimari · Bootstrap 5 · Poppins · (Faz 2+) EF Core + SQLite, Identity, SignalR

---

## Yol Haritası

| Faz | Kapsam | Durum |
|---|---|---|
| **Faz 1** | İskelet: proje yapısı, Program.cs, Area route'ları, _Layout, Kovan teması, Controller ve View'lar, örnek veri servisleri | ✅ Tamamlandı |
| Faz 2 | EF Core (SQLite), Entity ilişkileri, Migration, Seed, Identity (Ogrenci / KulupYoneticisi / Admin) | ⏳ |
| Faz 3 | Modül CRUD işlemleri ve iş kuralları (ödünç akışı, kontenjan, koltuk rezervasyonu) | ⏳ |
| Faz 4 | SignalR (sohbet, canlı teklif, bildirim) + BackgroundService (artırma kapatma) | ⏳ |
| Faz 5 | Admin paneli, güven puanı, şikâyet/moderasyon, son rötuşlar | ⏳ |

> **Veritabanı seçimi:** Faz 2'de **SQLite** kullanılacak. Kurulum gerektirmez, veritabanı tek bir dosyadır (`kovan.db`), ekipteki herkesin bilgisayarında aynı şekilde çalışır. İstenirse sadece bağlantı satırı değiştirilerek SQL Server/PostgreSQL'e geçilebilir.

---

# FAZ 1 – İSKELET

## 1. Bu fazda yapılanlar

- [x] ASP.NET Core 8 MVC projesi ve `Kovan.sln` çözüm dosyası
- [x] `Program.cs`: MVC, DI kayıtları, 5 Area için route, temalı 404/hata sayfaları
- [x] **5 Area** (Market, Events, Topics, Carpool, Admin) – her birinde `[Area("...")]` etiketli controller, `_ViewStart.cshtml` ve `_ViewImports.cshtml`
- [x] **Ortak `_Layout`**: navbar, bildirim zili (ViewComponent), kullanıcı menüsü, footer
- [x] **Kovan teması**: bal sarısı / antrasit / krem, petek (altıgen) motifleri, Poppins, yumuşak gölgeler, hover efektleri, tamamen responsive
- [x] Ana sayfada modüller **petek şeklinde dizilmiş kartlar**
- [x] **8 Controller, 18 View** (her controller'da en az 2 View)
- [x] **Dinamik Razor alanları**: `@foreach` ile ilan, etkinlik, oda, yolculuk listeleri; query string filtreleri; takvim ızgarası; `@switch` ile ilan türüne göre farklı aksiyon kutusu
- [x] **Veritabanına hazır yapı**: `Core/Entities` (POCO), `Core/Interfaces` (servis sözleşmeleri), `Core/Services/Sample` (örnek veri servisleri). Faz 2'de sadece servis kayıtları değişecek
- [x] Entity yerine **ViewModel** gönderimi, ince Controller'lar, Türkçe yorum satırları
- [x] Tekrar kullanılabilir parçalar: `_ListingCard`, `_EventCard`, `_PageHeader`, `_EmptyState` partial'ları + `NotificationBell` ViewComponent'i
- [x] Kategoriye göre varsayılan etkinlik kapak görselleri (SVG)

### Controller ve View listesi

| Controller | Konum | View'lar | Adres |
|---|---|---|---|
| HomeController | Controllers/ | Index, About, StatusCode, (Shared/Error) | `/`, `/Home/About` |
| AccountController | Controllers/ | Login, Register | `/Account/Login` |
| ProfileController | Controllers/ | Index, Edit | `/Profile` |
| ListingsController | Areas/Market | Index, Details | `/Market` |
| EventsController | Areas/Events | Index, Details, Calendar | `/Events`, `/Events/Events/Calendar` |
| RoomsController | Areas/Topics | Index, Room | `/Topics`, `/Topics/Rooms/Room/1` |
| RidesController | Areas/Carpool | Index, Details | `/Carpool` |
| DashboardController | Areas/Admin | Index, Users | `/Admin` |

## 2. Klasör ağacı

```
Kovan/
├── Kovan/
│   ├── Areas/
│   │   ├── Admin/
│   │   │   ├── Controllers/
│   │   │   │   └── DashboardController.cs
│   │   │   ├── Models/
│   │   │   │   └── AdminViewModels.cs
│   │   │   └── Views/
│   │   │       ├── Dashboard/
│   │   │       │   ├── Index.cshtml
│   │   │       │   └── Users.cshtml
│   │   │       ├── _ViewImports.cshtml
│   │   │       └── _ViewStart.cshtml
│   │   ├── Carpool/
│   │   │   ├── Controllers/
│   │   │   │   └── RidesController.cs
│   │   │   ├── Models/
│   │   │   │   └── CarpoolViewModels.cs
│   │   │   └── Views/
│   │   │       ├── Rides/
│   │   │       │   ├── Details.cshtml
│   │   │       │   └── Index.cshtml
│   │   │       ├── _ViewImports.cshtml
│   │   │       └── _ViewStart.cshtml
│   │   ├── Events/
│   │   │   ├── Controllers/
│   │   │   │   └── EventsController.cs
│   │   │   ├── Models/
│   │   │   │   └── EventsViewModels.cs
│   │   │   └── Views/
│   │   │       ├── Events/
│   │   │       │   ├── Calendar.cshtml
│   │   │       │   ├── Details.cshtml
│   │   │       │   └── Index.cshtml
│   │   │       ├── _ViewImports.cshtml
│   │   │       └── _ViewStart.cshtml
│   │   ├── Market/
│   │   │   ├── Controllers/
│   │   │   │   └── ListingsController.cs
│   │   │   ├── Models/
│   │   │   │   └── MarketViewModels.cs
│   │   │   └── Views/
│   │   │       ├── Listings/
│   │   │       │   ├── Details.cshtml
│   │   │       │   └── Index.cshtml
│   │   │       ├── _ViewImports.cshtml
│   │   │       └── _ViewStart.cshtml
│   │   └── Topics/
│   │       ├── Controllers/
│   │       │   └── RoomsController.cs
│   │       ├── Models/
│   │       │   └── TopicsViewModels.cs
│   │       └── Views/
│   │           ├── Rooms/
│   │           │   ├── Index.cshtml
│   │           │   └── Room.cshtml
│   │           ├── _ViewImports.cshtml
│   │           └── _ViewStart.cshtml
│   ├── Controllers/
│   │   ├── AccountController.cs
│   │   ├── HomeController.cs
│   │   └── ProfileController.cs
│   ├── Core/
│   │   ├── Entities/
│   │   │   ├── AppUser.cs
│   │   │   ├── BaseEntity.cs
│   │   │   ├── CampusEvent.cs
│   │   │   ├── Listing.cs
│   │   │   ├── Notification.cs
│   │   │   ├── Ride.cs
│   │   │   ├── TopicMessage.cs
│   │   │   └── TopicRoom.cs
│   │   ├── Enums/
│   │   │   ├── EventCategory.cs
│   │   │   ├── MarketEnums.cs
│   │   │   └── UserRole.cs
│   │   ├── Extensions/
│   │   │   └── ServiceCollectionExtensions.cs
│   │   ├── Helpers/
│   │   │   └── DisplayHelpers.cs
│   │   ├── Interfaces/
│   │   │   ├── IEventService.cs
│   │   │   ├── IListingService.cs
│   │   │   ├── INotificationService.cs
│   │   │   ├── IRideService.cs
│   │   │   ├── IStatsService.cs
│   │   │   ├── ITopicService.cs
│   │   │   └── IUserService.cs
│   │   └── Services/
│   │       └── Sample/
│   │           ├── SampleEventService.cs
│   │           ├── SampleListingService.cs
│   │           ├── SampleNotificationService.cs
│   │           ├── SampleRideService.cs
│   │           ├── SampleStatsService.cs
│   │           ├── SampleTopicService.cs
│   │           └── SampleUserService.cs
│   ├── Data/
│   │   ├── SampleData/
│   │   │   └── SampleDataStore.cs
│   │   └── README.md
│   ├── Hubs/
│   │   └── README.md
│   ├── Models/
│   │   ├── AccountViewModels.cs
│   │   ├── CardViewModels.cs
│   │   ├── ErrorViewModel.cs
│   │   ├── HomeViewModels.cs
│   │   └── ProfileViewModels.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── ViewComponents/
│   │   └── NotificationBellViewComponent.cs
│   ├── Views/
│   │   ├── Account/
│   │   │   ├── Login.cshtml
│   │   │   └── Register.cshtml
│   │   ├── Home/
│   │   │   ├── About.cshtml
│   │   │   ├── Index.cshtml
│   │   │   └── StatusCode.cshtml
│   │   ├── Profile/
│   │   │   ├── Edit.cshtml
│   │   │   └── Index.cshtml
│   │   ├── Shared/
│   │   │   ├── Components/
│   │   │   │   └── NotificationBell/
│   │   │   │       └── Default.cshtml
│   │   │   ├── Partials/
│   │   │   │   ├── _EmptyState.cshtml
│   │   │   │   ├── _EventCard.cshtml
│   │   │   │   ├── _ListingCard.cshtml
│   │   │   │   └── _PageHeader.cshtml
│   │   │   ├── _Layout.cshtml
│   │   │   └── Error.cshtml
│   │   ├── _ViewImports.cshtml
│   │   └── _ViewStart.cshtml
│   ├── wwwroot/
│   │   ├── css/
│   │   │   ├── modules/
│   │   │   │   ├── account.css
│   │   │   │   ├── admin.css
│   │   │   │   ├── carpool.css
│   │   │   │   ├── events.css
│   │   │   │   ├── home.css
│   │   │   │   ├── market.css
│   │   │   │   ├── profile.css
│   │   │   │   └── topics.css
│   │   │   └── site.css
│   │   ├── img/
│   │   │   ├── defaults/
│   │   │   │   ├── event-art.svg
│   │   │   │   ├── event-career.svg
│   │   │   │   ├── event-social.svg
│   │   │   │   ├── event-sports.svg
│   │   │   │   └── event-technology.svg
│   │   │   ├── favicon.svg
│   │   │   └── logo.svg
│   │   └── js/
│   │       └── site.js
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── Kovan.csproj
│   └── Program.cs
└── Kovan.sln
```

### Katmanlar ne işe yarıyor?

| Klasör | Görev |
|---|---|
| `Areas/<Modül>/` | Her modülün kendi Controller, ViewModel ve View'ları. Sunumda modül modül gösterilebilir. |
| `Controllers/`, `Views/` | Modüllerden bağımsız genel sayfalar (Ana sayfa, Hesap, Profil) |
| `Core/Entities` | Veritabanı tablolarına dönüşecek sınıflar (Faz 2'de EF Core) |
| `Core/Interfaces` | Servis sözleşmeleri. Controller'lar sadece bunları bilir. |
| `Core/Services/Sample` | Faz 1 örnek veri servisleri. Faz 2'de EF Core servisleri ile değişecek. |
| `Core/Extensions` | `AddKovanServices()` – tüm DI kayıtları tek yerde |
| `Core/Helpers` | Enum → Türkçe ad / ikon, tarih ve para biçimlendirme |
| `Data/SampleData` | Bellekte tutulan örnek veri (tarihler bugüne göre üretilir) |
| `Hubs/` | Faz 4 SignalR hub'ları için ayrıldı |
| `ViewComponents/` | Navbar'daki bildirim zili |
| `Views/Shared/Partials` | Tekrar kullanılan Razor parçaları (Program.cs'de arama yoluna eklendi) |
| `wwwroot/css/modules/` | Her modülün kendi CSS dosyası (Area'daki `_ViewStart` otomatik yükler) |

## 3. Dosyalar

Tüm dosyalar bu repoda tam ve eksiksiz olarak bulunur (yukarıdaki ağaçtaki her dosya). Sunumda en çok bakılacak dosyalar:

| Dosya | Neden önemli |
|---|---|
| `Kovan/Program.cs` | DI, middleware sırası, Area route'ları |
| `Kovan/Core/Extensions/ServiceCollectionExtensions.cs` | Interface → implementasyon eşleşmesi (Faz 2'de değişecek tek yer) |
| `Kovan/Areas/Market/Controllers/ListingsController.cs` | `[Area]` etiketi, ince controller, query string filtresi |
| `Kovan/Areas/Market/Views/Listings/Index.cshtml` | `@foreach` ile dinamik liste ve filtreler |
| `Kovan/Areas/Events/Models/EventsViewModels.cs` | Takvim ızgarasını hazırlayan ViewModel |
| `Kovan/Views/Shared/_Layout.cshtml` | Ortak şablon, modül CSS'inin otomatik yüklenmesi |
| `Kovan/ViewComponents/NotificationBellViewComponent.cs` | ViewComponent örneği |
| `Kovan/Areas/*/Views/_ViewStart.cshtml` | Ortak layout + modül bilgisinin layout'a aktarılması |
| `Kovan/wwwroot/css/site.css` | Tema değişkenleri, petek motifleri |
| `Kovan/wwwroot/css/modules/home.css` | Petek (honeycomb) ızgarası |

## 4. Çalıştırma

Gerekenler: **.NET 8 SDK** (`dotnet --version` → 8.x). İnternet bağlantısı (Bootstrap, ikonlar ve font CDN'den gelir).

```bash
# Repoyu indir
git clone https://github.com/highlvmami/Kovan.git
cd Kovan

# Derle ve çalıştır
dotnet build
dotnet run --project Kovan

# Tarayıcıda aç:  http://localhost:5080
```

Değişiklik yaptıkça otomatik yenilenmesi için:

```bash
dotnet watch --project Kovan
```

**Visual Studio 2022:** `Kovan.sln` dosyasını aç → F5.
**VS Code / Rider:** klasörü aç → `dotnet run --project Kovan`.

> Faz 1'de veritabanı ve migration yok. Faz 2'de şu komutlar eklenecek:
> `dotnet tool install --global dotnet-ef` → `dotnet ef migrations add InitialCreate --project Kovan` → `dotnet ef database update --project Kovan`

### Denenecek adresler

- `/` – petek ana sayfa
- `/Market?type=Sale` · `/Market?courseCode=YZM 3215` – filtreli ilan listesi
- `/Market/Listings/Details/3` – açık artırma detayı
- `/Events/Events/Calendar` – takvim (mobilde liste görünümü)
- `/Topics/Rooms/Room/1` – sohbet ekranı
- `/Carpool?from=Turgutlu` – yolculuk arama
- `/Admin` – yönetim paneli
- `/olmayan-sayfa` – temalı 404 sayfası

## 5. Sunum özeti (hocaya anlatılacaklar)

1. **Modüler mimari:** Her modül ayrı bir **Area** (Market, Events, Topics, Carpool, Admin). Route'lar `Program.cs`'de `MapAreaControllerRoute` ile tanımlandı, controller'larda `[Area("...")]` etiketi var. Hoca yeni bir istek getirirse yeni bir Area klasörü açmak yeterli.
2. **Katmanlı yapı ve DI:** Controller'lar ince; iş mantığı `Core/Services` katmanında ve interface arkasında. Servisler constructor'dan enjekte ediliyor. Faz 1'de örnek veri servisleri çalışıyor; Faz 2'de **sadece** `AddKovanServices()` içindeki kayıtlar EF Core servisleriyle değişecek, Controller ve View'lara dokunulmayacak.
3. **ViewModel kullanımı:** View'lara entity değil ViewModel gidiyor (`ListingCardViewModel.FromEntity` gibi). Tekrar kullanılan parçalar partial (`_ListingCard`, `_EventCard`) ve ViewComponent (`NotificationBell`) olarak yazıldı.
4. **Dinamik Razor:** Listeler `@foreach` ile servisten gelen veriden üretiliyor; filtreler query string ile çalışıyor; takvim 42 hücrelik ızgara olarak ViewModel'de hesaplanıyor; ilan detayında `@switch` ile türe göre (ödünç / satış / artırma) farklı arayüz gösteriliyor.
5. **Tasarım:** Ortak `_Layout` + Kovan teması (bal sarısı, antrasit, krem, altıgen motifleri, Poppins). Her modülün kendi CSS dosyası `_ViewStart` üzerinden otomatik yükleniyor. Tüm sayfalar mobil, tablet ve masaüstünde responsive; 404 ve hata sayfaları bile temalı.
