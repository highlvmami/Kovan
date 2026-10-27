using Kovan.Areas.Carpool.Models;
using Kovan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Areas.Carpool.Controllers;

// ===== [CONTROLLER] RidesController · Adres: /Carpool · View'ları: Index, Details (Areas/Carpool/Views/Rides/) =====
/// <summary>CARPOOL MODÜLÜ – Ortak yolculuk ilanları.</summary>
[Area("Carpool")]
public class RidesController : Controller
{
    private readonly IRideService _rides;

    public RidesController(IRideService rides) => _rides = rides;

    // GET: /Carpool?from=Turgutlu&to=İzmir&date=2026-10-20
    // [CONTROLLER → VIEW] Index() action'ı → Areas/Carpool/Views/Rides/Index.cshtml sayfasını döndürür
    public async Task<IActionResult> Index(string? from, string? to, DateTime? date)
    {
        var items = await _rides.SearchAsync(from, to, date);
        var model = new RideIndexViewModel
        {
            From = from,
            To = to,
            Date = date,
            Places = _rides.GetKnownPlaces().ToList(),
            Rides = items.Select(RideCardViewModel.FromEntity).ToList()
        };
        return View(model);
    }

    // GET: /Carpool/Rides/Details/1
    // [CONTROLLER → VIEW] Details() action'ı → Areas/Carpool/Views/Rides/Details.cshtml sayfasını döndürür
    public async Task<IActionResult> Details(int id)
    {
        var ride = await _rides.GetByIdAsync(id);
        if (ride is null) return NotFound();
        return View(RideDetailsViewModel.FromEntity(ride));
    }
}
