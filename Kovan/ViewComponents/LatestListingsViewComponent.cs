using Kovan.Core.Helpers;
using Kovan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kovan.ViewComponents;

/// <summary>
/// "Kampüste yeni ilanlar" kutusu.
/// Market dışındaki sayfaların yan sütununda (etkinlik detayı, sohbet odası,
/// yolculuk detayı) Market'e çapraz bağlantı verir.
/// Partial view'dan farkı: veriyi kendisi IListingService'ten çeker, yani onu kullanan
/// sayfaların controller'ları ilan verisi göndermek zorunda kalmaz.
///
/// Kullanım: @await Component.InvokeAsync("LatestListings", new { count = 4 })
/// Görünümü:  Views/Shared/Components/LatestListings/Default.cshtml
/// </summary>
public class LatestListingsViewComponent : ViewComponent
{
    private readonly IListingService _listings;

    public LatestListingsViewComponent(IListingService listings) => _listings = listings;

    public async Task<IViewComponentResult> InvokeAsync(int count = 4)
    {
        var items = await _listings.GetLatestAsync(count);

        var model = items.Select(l => new MiniListingViewModel(
            l.Id,
            l.Title,
            l.Category.ToIcon(),
            l.Type.ToDisplay(),
            l.CreatedAt.ToRelative(),
            // Son 1 saatte eklenenlere "YENİ" rozeti
            IsNew: l.CreatedAt > DateTime.Now.AddHours(-1))).ToList();

        return View(model);
    }
}

public record MiniListingViewModel(int Id, string Title, string CategoryIcon, string TypeText, string TimeText, bool IsNew);
