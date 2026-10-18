using Kovan.Areas.Market.Models;
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

    public ListingsController(IListingService listings) => _listings = listings;

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
}
