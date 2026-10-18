using Kovan.Core.Entities;
using Kovan.Core.Helpers;

namespace Kovan.Models;

// ============================================================================
//  ORTAK KART VIEWMODEL'LERİ
//  Ana sayfada da, modül sayfalarında da aynı kart partial'ı kullanıldığı için
//  bu ViewModel'ler ortak Models klasöründe durur.
//  KURAL: View'a entity değil ViewModel gider. Entity → ViewModel dönüşümü
//  her ViewModel'in FromEntity metodunda yapılır.
// ============================================================================

/// <summary>Views/Shared/Partials/_ListingCard.cshtml için.</summary>
public class ListingCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string TypeText { get; set; } = string.Empty;
    public string TypeIcon { get; set; } = string.Empty;
    public string TypeBadgeClass { get; set; } = string.Empty;
    public string CategoryText { get; set; } = string.Empty;
    public string CategoryIcon { get; set; } = string.Empty;
    public string? CourseCode { get; set; }
    public string PriceText { get; set; } = string.Empty;   // "75 ₺", "Ücretsiz ödünç", "Teklif: 420 ₺"
    public string? AuctionRemaining { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public double OwnerTrust { get; set; }
    public string CreatedText { get; set; } = string.Empty;

    public static ListingCardViewModel FromEntity(Listing l) => new()
    {
        Id = l.Id,
        Title = l.Title,
        TypeText = l.Type.ToDisplay(),
        TypeIcon = l.Type.ToIcon(),
        TypeBadgeClass = l.Type.ToBadgeClass(),
        CategoryText = l.Category.ToDisplay(),
        CategoryIcon = l.Category.ToIcon(),
        CourseCode = l.CourseCode,
        PriceText = l.Type switch
        {
            Core.Enums.ListingType.Lend => "Ücretsiz ödünç",
            Core.Enums.ListingType.Auction => "Teklif: " + (l.CurrentBid ?? l.Price ?? 0).ToMoney(),
            _ => (l.Price ?? 0).ToMoney()
        },
        AuctionRemaining = l.AuctionEndsAt?.ToRemaining(),
        OwnerName = l.Owner?.FullName ?? "-",
        OwnerTrust = l.Owner?.TrustScore ?? 0,
        CreatedText = l.CreatedAt.ToRelative()
    };
}

/// <summary>Views/Shared/Partials/_EventCard.cshtml için.</summary>
public class EventCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CategoryText { get; set; } = string.Empty;
    public string CategoryIcon { get; set; } = string.Empty;
    public string CoverUrl { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public string Day { get; set; } = string.Empty;
    public string MonthShort { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? ClubName { get; set; }
    public int Capacity { get; set; }
    public int Registered { get; set; }
    public bool IsFull { get; set; }

    /// <summary>Kontenjan doluluk yüzdesi (ilerleme çubuğu için).</summary>
    public int FillPercent => Capacity == 0 ? 0 : Math.Min(100, Registered * 100 / Capacity);

    public static EventCardViewModel FromEntity(CampusEvent e) => new()
    {
        Id = e.Id,
        Title = e.Title,
        CategoryText = e.Category.ToDisplay(),
        CategoryIcon = e.Category.ToIcon(),
        // Kapak görseli yoksa kategoriye göre varsayılan görsel
        CoverUrl = string.IsNullOrEmpty(e.CoverImageUrl) ? e.Category.DefaultCover() : e.CoverImageUrl,
        DateText = e.StartsAt.ToDateTime(),
        Day = e.StartsAt.Day.ToString(),
        MonthShort = e.StartsAt.ToString("MMM", DisplayHelpers.Tr).ToUpper(DisplayHelpers.Tr),
        Location = e.Location,
        ClubName = e.ClubName,
        Capacity = e.Capacity,
        Registered = e.RegisteredCount,
        IsFull = e.IsFull
    };
}

/// <summary>Sayfa başlığı partial'ı (_PageHeader) için.</summary>
public class PageHeaderViewModel
{
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string Icon { get; set; } = "bi-hexagon";
    public string? ModuleName { get; set; }
}

/// <summary>Boş durum partial'ı (_EmptyState) için.</summary>
public class EmptyStateViewModel
{
    public string Icon { get; set; } = "bi-inbox";
    public string Title { get; set; } = "Burada henüz bir şey yok";
    public string? Message { get; set; }
}
