using Kovan.Areas.Events.Models;
using Kovan.Core.Enums;
using Kovan.Core.Interfaces;
using Kovan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Areas.Events.Controllers;

// ===== [CONTROLLER] EventsController · Adres: /Events · View'ları: Index, Details, Calendar (Areas/Events/Views/Events/) =====
/// <summary>EVENTS MODÜLÜ – Etkinlik listesi, detay ve takvim görünümü.</summary>
[Area("Events")]
public class EventsController : Controller
{
    private readonly IEventService _events;

    public EventsController(IEventService events) => _events = events;

    // GET: /Events  veya  /Events?category=Technology
    // [CONTROLLER → VIEW] Index() action'ı → Areas/Events/Views/Events/Index.cshtml sayfasını döndürür
    public async Task<IActionResult> Index(EventCategory? category)
    {
        var items = await _events.GetUpcomingAsync(category);
        var model = new EventIndexViewModel
        {
            SelectedCategory = category,
            Events = items.Select(EventCardViewModel.FromEntity).ToList()
        };
        return View(model);
    }

    // GET: /Events/Events/Details/1
    // [CONTROLLER → VIEW] Details() action'ı → Areas/Events/Views/Events/Details.cshtml sayfasını döndürür
    public async Task<IActionResult> Details(int id)
    {
        var ev = await _events.GetByIdAsync(id);
        if (ev is null) return NotFound();
        return View(EventDetailsViewModel.FromEntity(ev));
    }

    // GET: /Events/Events/Calendar?year=2026&month=11
    // [CONTROLLER → VIEW] Calendar() action'ı → Areas/Events/Views/Events/Calendar.cshtml sayfasını döndürür
    public async Task<IActionResult> Calendar(int? year, int? month)
    {
        int y = year ?? DateTime.Today.Year;
        int m = month is >= 1 and <= 12 ? month.Value : DateTime.Today.Month;

        // Takvim ızgarası önceki/sonraki aydan da gün gösterdiği için 3 ayın etkinliklerini alıyoruz.
        var current = new DateTime(y, m, 1);
        var events = new List<Core.Entities.CampusEvent>();
        foreach (var d in new[] { current.AddMonths(-1), current, current.AddMonths(1) })
            events.AddRange(await _events.GetByMonthAsync(d.Year, d.Month));

        return View(CalendarViewModel.Build(y, m, events));
    }
}
