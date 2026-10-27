using Kovan.Areas.Admin.Models;
using Kovan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Areas.Admin.Controllers;

// ===== [CONTROLLER] DashboardController · Adres: /Admin · View'ları: Index, Users (Areas/Admin/Views/Dashboard/) =====
/// <summary>
/// ADMIN MODÜLÜ – Yönetim paneli.
/// Faz 2'de [Authorize(Roles = Roles.Admin)] eklenerek sadece Admin rolüne açılacak.
/// </summary>
[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IStatsService _stats;
    private readonly IUserService _users;

    public DashboardController(IStatsService stats, IUserService users)
    {
        _stats = stats;
        _users = users;
    }

    // GET: /Admin
    // [CONTROLLER → VIEW] Index() action'ı → Areas/Admin/Views/Dashboard/Index.cshtml sayfasını döndürür
    public async Task<IActionResult> Index()
    {
        var stats = await _stats.GetStatsAsync();
        var model = new DashboardViewModel
        {
            Stats = stats,
            ModuleBars = new()
            {
                ("Market", stats.ActiveListingCount, "bar-market"),
                ("Etkinlik", stats.UpcomingEventCount, "bar-events"),
                ("Oda", stats.RoomCount, "bar-topics"),
                ("Yolculuk", stats.RideCount, "bar-carpool"),
            },
            RecentActivities = new()
            {
                "Mekanik Klavye ilanına yeni teklif verildi",
                "Halı Saha Turnuvası kontenjanı doldu",
                "Final Haftası odasında 1 mesaj şikâyet edildi",
                "Manisa → Ankara yolculuğu oluşturuldu",
            }
        };
        return View(model);
    }

    // GET: /Admin/Dashboard/Users
    // [CONTROLLER → VIEW] Users() action'ı → Areas/Admin/Views/Dashboard/Users.cshtml sayfasını döndürür
    public async Task<IActionResult> Users()
    {
        var users = await _users.GetAllAsync();
        var model = users.Select(u => new AdminUserRowViewModel
        {
            Id = u.Id,
            FullName = u.FullName,
            Initials = u.Initials,
            Email = u.Email,
            Department = u.Department,
            Role = u.Role,
            TrustScore = u.TrustScore
        }).ToList();
        return View(model);
    }
}
