using Kovan.Core.Enums;

namespace Kovan.Core.Entities;

/// <summary>
/// Market modülündeki ilan (ödünç / satış / açık artırma).
/// </summary>
public class Listing : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ListingType Type { get; set; }
    public ListingCategory Category { get; set; }

    /// <summary>Ders kaynakları için ders kodu (ör. "YZM 3215"). Filtrelemede kullanılır.</summary>
    public string? CourseCode { get; set; }

    /// <summary>Satışta sabit fiyat, açık artırmada başlangıç fiyatı. Ödünçte null.</summary>
    public decimal? Price { get; set; }

    /// <summary>Açık artırmada şu anki en yüksek teklif.</summary>
    public decimal? CurrentBid { get; set; }
    public int BidCount { get; set; }

    /// <summary>Açık artırma bitiş zamanı. Faz 4'te background job bu alana bakıp artırmayı kapatacak.</summary>
    public DateTime? AuctionEndsAt { get; set; }

    public string? ImageUrl { get; set; }
    public string Condition { get; set; } = "İyi";   // Kullanım durumu
    public bool IsActive { get; set; } = true;

    // İlişki: İlanın sahibi (Faz 2'de foreign key olacak)
    public int OwnerId { get; set; }
    public AppUser? Owner { get; set; }

    // Faz 4'te eşzamanlı tekliflerde concurrency kontrolü için:
    // [Timestamp] public byte[] RowVersion { get; set; }
}
