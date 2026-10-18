using Kovan.Core.Interfaces;
using Kovan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.Controllers;

/// <summary>Kullanıcı profili ve Güven Puanı sayfaları.</summary>
public class ProfileController : Controller
{
    private readonly IUserService _users;
    private readonly IListingService _listings;

    public ProfileController(IUserService users, IListingService listings)
    {
        _users = users;
        _listings = listings;
    }

    // GET: /Profile  veya  /Profile/Index/3 (başka bir kullanıcının profili)
    public async Task<IActionResult> Index(int? id)
    {
        var user = id.HasValue ? await _users.GetByIdAsync(id.Value) : await _users.GetCurrentUserAsync();
        if (user is null) return NotFound();

        var all = await _listings.GetListingsAsync(new ListingFilter());

        var model = ProfileViewModel.FromEntity(user);
        model.MyListings = all.Where(l => l.OwnerId == user.Id)
                              .Select(ListingCardViewModel.FromEntity)
                              .ToList();

        // Güven puanı dağılımı: Faz 5'te modüllerdeki gerçek puanlamalardan hesaplanacak.
        model.TrustBreakdown = new()
        {
            ("Market", "bi-shop", Math.Min(5, user.TrustScore + 0.1)),
            ("Etkinlikler", "bi-calendar-event", user.TrustScore),
            ("Yolculuk", "bi-car-front", Math.Max(0, user.TrustScore - 0.2)),
        };

        ViewData["IsOwnProfile"] = !id.HasValue || id == (await _users.GetCurrentUserAsync()).Id;
        return View(model);
    }

    // GET: /Profile/Edit
    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var user = await _users.GetCurrentUserAsync();
        var model = new ProfileEditViewModel { FullName = user.FullName, Department = user.Department, Grade = user.Grade };
        return View(model);
    }

    // POST: /Profile/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProfileEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // TODO (Faz 3): _users.UpdateProfileAsync(model)
        TempData["Info"] = "Profil kaydetme Faz 3'te veritabanına bağlanacak.";
        return RedirectToAction(nameof(Index));
    }
}
