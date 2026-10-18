using System.Diagnostics;
using Kovan.Core.Interfaces;
using Kovan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Controllers;

/// <summary>
/// Genel sayfalar: Ana sayfa (petek modüller), Hakkında, Hata ve 404 sayfaları.
/// Controller İNCE tutulur: veriyi servislerden alır, ViewModel'e koyar, View'a verir.
/// </summary>
public class HomeController : Controller
{
    // Servisler constructor üzerinden enjekte edilir (Dependency Injection).
    private readonly IListingService _listings;
    private readonly IEventService _events;
    private readonly IUserService _users;
    private readonly IStatsService _stats;

    public HomeController(IListingService listings, IEventService events, IUserService users, IStatsService stats)
    {
        _listings = listings;
        _events = events;
        _users = users;
        _stats = stats;
    }

    // GET: /
    public async Task<IActionResult> Index()
    {
        var user = await _users.GetCurrentUserAsync();
        var stats = await _stats.GetStatsAsync();
        var latest = await _listings.GetLatestAsync(4);
        var upcoming = await _events.GetUpcomingAsync();

        var model = new HomeIndexViewModel
        {
            UserFirstName = user.FullName.Split(' ')[0],
            Stats = stats,
            LatestListings = latest.Select(ListingCardViewModel.FromEntity).ToList(),
            UpcomingEvents = upcoming.Take(3).Select(EventCardViewModel.FromEntity).ToList(),

            // Petek kartları: yeni bir modül eklendiğinde buraya bir satır eklemek yeterli.
            Modules = new()
            {
                new() { Name = "Market", Description = "Ödünç al, sat, açık artırmaya çık.", Icon = "bi-shop", Area = "Market", Controller = "Listings", ColorClass = "hex-market", Badge = $"{stats.ActiveListingCount} ilan" },
                new() { Name = "Etkinlikler", Description = "Kampüste neler oluyor?", Icon = "bi-calendar-event", Area = "Events", Controller = "Events", ColorClass = "hex-events", Badge = $"{stats.UpcomingEventCount} etkinlik" },
                new() { Name = "Konular", Description = "Konu bazlı sohbet odaları.", Icon = "bi-chat-dots", Area = "Topics", Controller = "Rooms", ColorClass = "hex-topics", Badge = $"{stats.RoomCount} oda" },
                new() { Name = "Yolculuk", Description = "Ortak yolculuk, ortak masraf.", Icon = "bi-car-front", Area = "Carpool", Controller = "Rides", ColorClass = "hex-carpool", Badge = $"{stats.RideCount} yolculuk" },
                new() { Name = "Profilim", Description = "Güven puanın ve ilanların.", Icon = "bi-person-badge", Area = "", Controller = "Profile", ColorClass = "hex-profile" },
                new() { Name = "Yönetim", Description = "Admin paneli ve istatistikler.", Icon = "bi-shield-lock", Area = "Admin", Controller = "Dashboard", ColorClass = "hex-admin" },
            }
        };

        return View(model);
    }

    // GET: /Home/About
    public IActionResult About()
    {
        var team = new List<TeamMemberViewModel>
        {
            new("Ekip Üyesi 1", "Market & Ortak Altyapı", "bi-shop"),
            new("Ekip Üyesi 2", "Events & Topics", "bi-calendar-event"),
            new("Ekip Üyesi 3", "Carpool & Admin", "bi-car-front"),
        };
        return View(team);
    }

    // GET: /Home/StatusCode?code=404 → Program.cs'deki UseStatusCodePagesWithReExecute buraya yönlendirir.
    [Route("Home/StatusCode")]
    public IActionResult StatusCodePage(int code)
    {
        var model = new ErrorViewModel
        {
            StatusCode = code,
            Title = code == 404 ? "Bu petek gözü boş!" : $"Hata {code}",
            Message = code == 404
                ? "Aradığın sayfa kovanda bulunamadı. Belki başka bir göze uçmuştur."
                : "İsteğin işlenirken bir sorun oluştu."
        };
        return View("StatusCode", model);
    }

    // GET: /Home/Error → Üretim ortamında yakalanmayan hatalar buraya düşer.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = 500 });
    }
}
