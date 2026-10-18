// ============================================================================
//  KOVAN 🐝 – Modüler Kampüs Platformu
//  Program.cs: Uygulamanın giriş noktası.
//  Burada iki şey yapılır:
//    1) Servisler DI (Dependency Injection) konteynerine kaydedilir.
//    2) HTTP istek hattı (middleware pipeline) ve route'lar tanımlanır.
// ============================================================================

using Kovan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// 1) SERVİS KAYITLARI
// ---------------------------------------------------------------------------

// MVC: Controller + Razor View desteği
builder.Services.AddControllersWithViews();

// Ortak partial'ları (Views/Shared/Partials) her yerden sadece adıyla
// çağırabilmek için Razor'un arama yollarına ekliyoruz.
// Böylece hem ana uygulamada hem Area'larda <partial name="_ListingCard" /> yeterli olur.
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Views/Shared/Partials/{0}" + RazorViewEngine.ViewExtension);
    options.AreaViewLocationFormats.Add("/Views/Shared/Partials/{0}" + RazorViewEngine.ViewExtension);
});

// Kovan'ın iş mantığı servisleri (Core katmanı).
// Faz 1'de örnek veri servisleri kullanılır; Faz 2'de SADECE bu metodun içi
// EF Core servisleriyle değiştirilecek, Controller'lara dokunulmayacak.
builder.Services.AddKovanServices();

var app = builder.Build();

// ---------------------------------------------------------------------------
// 2) MIDDLEWARE HATTI
// ---------------------------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    // Üretimde hata olursa kullanıcıya temalı hata sayfası gösterilir.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 404 gibi durum kodlarında da düz beyaz sayfa yerine temalı sayfa gösterilir.
app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();      // wwwroot (css, js, img)
app.UseRouting();
app.UseAuthorization();    // Faz 2'de Identity ile birlikte UseAuthentication da eklenecek

// ---------------------------------------------------------------------------
// 3) ROUTE TANIMLARI
//    Önce Area route'ları, en son varsayılan route tanımlanır.
//    Her modülün "ana controller"ı adresi sade olsun diye ayrıca belirtilir:
//      /Market        → Market/Listings/Index
//      /Events        → Events/Events/Index
//      /Topics        → Topics/Rooms/Index
//      /Carpool       → Carpool/Rides/Index
//      /Admin         → Admin/Dashboard/Index
// ---------------------------------------------------------------------------

app.MapAreaControllerRoute(
    name: "market",
    areaName: "Market",
    pattern: "Market/{controller=Listings}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "events",
    areaName: "Events",
    pattern: "Events/{controller=Events}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "topics",
    areaName: "Topics",
    pattern: "Topics/{controller=Rooms}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "carpool",
    areaName: "Carpool",
    pattern: "Carpool/{controller=Rides}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

// Genel Area route'u: ileride hoca yeni modül isterse (ör. "Lostfound"),
// sadece Areas/Lostfound klasörünü açmak yeterli olur.
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Ana uygulama (Home, Account, Profile)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
