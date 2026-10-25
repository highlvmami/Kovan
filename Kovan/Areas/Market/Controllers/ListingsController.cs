using Kovan.Areas.Market.Models;
using Kovan.Core.Entities;
using Kovan.Core.Enums;
using Kovan.Core.Interfaces;
using Kovan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Areas.Market.Controllers;

/// <summary>
/// MARKET MODÜLÜ – İlanlar
/// [Area("Market")] etiketi bu controller'ın Areas/Market klasörüne ait olduğunu söyler.
/// View'lar Areas/Market/Views/Listings/ altında aranır.
/// </summary>
[Area("Market")]
public class ListingsController : Controller
{
    private readonly IListingService _listings;
    private readonly IUserService _users;
    private readonly INotificationService _notifications;

    public ListingsController(IListingService listings, IUserService users, INotificationService notifications)
    {
        _listings = listings;
        _users = users;
        _notifications = notifications;
    }

    // GET: /Market
    // GET: /Market/Listings?type=Sale&category=CourseMaterial&courseCode=YZM 3215
    // Filtre parametreleri query string'den model binding ile otomatik gelir.
    public async Task<IActionResult> Index(ListingType? type, ListingCategory? category, string? courseCode, string? search)
    {
        var items = await _listings.GetListingsAsync(new ListingFilter(type, category, courseCode, search));

        var model = new ListingIndexViewModel
        {
            Type = type,
            Category = category,
            CourseCode = courseCode,
            Search = search,
            CourseCodes = (await _listings.GetCourseCodesAsync()).ToList(),
            Listings = items.Select(ListingCardViewModel.FromEntity).ToList()
        };
        return View(model);
    }

    // GET: /Market/Listings/Details/3
    public async Task<IActionResult> Details(int id)
    {
        var listing = await _listings.GetByIdAsync(id);
        if (listing is null) return NotFound();   // → temalı 404 sayfası

        var model = ListingDetailsViewModel.FromEntity(listing);

        // Aynı kategoriden benzer ilanlar
        var similar = await _listings.GetListingsAsync(new ListingFilter(Category: listing.Category));
        model.Similar = similar.Where(l => l.Id != id).Take(3).Select(ListingCardViewModel.FromEntity).ToList();

        return View(model);
    }

    // GET: /Market/Listings/Create  → "İlan Ver" formu (HtmlHelper'larla çizilir)
    [HttpGet]
    public IActionResult Create() => View(new ListingCreateViewModel());

    // POST: /Market/Listings/Create
    // Formdaki alanlar model binding ile ListingCreateViewModel'e dolar.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ListingCreateViewModel model)
    {
        // DataAnnotations + Validate() kuralları geçmezse form, girilen değerler ve hatalarla geri döner.
        if (!ModelState.IsValid) return View(model);

        var user = await _users.GetCurrentUserAsync();
        var listing = await _listings.AddAsync(model.ToEntity(user.Id));

        // Ortak bildirim merkezi: navbar'daki zil (ViewComponent) bu kaydı gösterir.
        await _notifications.AddAsync(new Notification
        {
            UserId = user.Id,
            Title = "İlanın yayında",
            Message = $"\"{listing.Title}\" Market'te listelendi.",
            Icon = "bi-megaphone",
            Module = "Market",
            Url = Url.Action(nameof(Details), new { id = listing.Id })
        });

        // PRG (Post-Redirect-Get): sayfa yenilenince form tekrar gönderilmesin.
        TempData["Info"] = "İlanın yayınlandı! Market listesinde, ana sayfada ve profilinde de görünüyor.";
        return RedirectToAction(nameof(Details), new { id = listing.Id });
    }
}
