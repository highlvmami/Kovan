using System.Globalization;
using Kovan.Core.Enums;

namespace Kovan.Core.Helpers;

/// <summary>
/// Enum'ların Türkçe görünen adları, ikonları ve renkleri.
/// View'larda "Lend" yerine "Ödünç Ver" yazabilmek için kullanılır.
/// </summary>
public static class DisplayHelpers
{
    public static readonly CultureInfo Tr = new("tr-TR");

    // ---------------- Market ----------------
    public static string ToDisplay(this ListingType type) => type switch
    {
        ListingType.Lend => "Ödünç",
        ListingType.Sale => "Satılık",
        ListingType.Auction => "Açık Artırma",
        _ => type.ToString()
    };

    public static string ToIcon(this ListingType type) => type switch
    {
        ListingType.Lend => "bi-arrow-left-right",
        ListingType.Sale => "bi-tag",
        ListingType.Auction => "bi-hammer",
        _ => "bi-box"
    };

    /// <summary>Rozet için CSS sınıfı (site.css'te tanımlı).</summary>
    public static string ToBadgeClass(this ListingType type) => type switch
    {
        ListingType.Lend => "kv-badge-lend",
        ListingType.Sale => "kv-badge-sale",
        ListingType.Auction => "kv-badge-auction",
        _ => "kv-badge"
    };

    public static string ToDisplay(this ListingCategory category) => category switch
    {
        ListingCategory.CourseMaterial => "Ders Kaynakları",
        ListingCategory.Electronics => "Elektronik",
        ListingCategory.Equipment => "Ekipman",
        ListingCategory.Other => "Diğer",
        _ => category.ToString()
    };

    public static string ToIcon(this ListingCategory category) => category switch
    {
        ListingCategory.CourseMaterial => "bi-book",
        ListingCategory.Electronics => "bi-laptop",
        ListingCategory.Equipment => "bi-tools",
        _ => "bi-box-seam"
    };

    // ---------------- Events ----------------
    public static string ToDisplay(this EventCategory category) => category switch
    {
        EventCategory.Technology => "Teknoloji",
        EventCategory.Social => "Sosyal",
        EventCategory.Sports => "Spor",
        EventCategory.Career => "Kariyer",
        EventCategory.Art => "Sanat & Kültür",
        _ => category.ToString()
    };

    public static string ToIcon(this EventCategory category) => category switch
    {
        EventCategory.Technology => "bi-cpu",
        EventCategory.Social => "bi-people",
        EventCategory.Sports => "bi-trophy",
        EventCategory.Career => "bi-briefcase",
        EventCategory.Art => "bi-palette",
        _ => "bi-calendar-event"
    };

    /// <summary>
    /// Kapak görseli yoksa kategoriye göre varsayılan görsel.
    /// Dosyalar: wwwroot/img/defaults/event-technology.svg vb.
    /// </summary>
    public static string DefaultCover(this EventCategory category) =>
        $"/img/defaults/event-{category.ToString().ToLowerInvariant()}.svg";

    // ---------------- Genel ----------------
    public static string ToMoney(this decimal value) => value.ToString("N0", Tr) + " ₺";

    public static string ToShortDate(this DateTime date) => date.ToString("d MMMM yyyy", Tr);

    public static string ToDateTime(this DateTime date) => date.ToString("d MMMM, ddd HH:mm", Tr);

    /// <summary>"5 dk önce", "2 saat önce" gibi göreli zaman.</summary>
    public static string ToRelative(this DateTime date)
    {
        var diff = DateTime.Now - date;
        if (diff.TotalMinutes < 1) return "az önce";
        if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} dk önce";
        if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} saat önce";
        if (diff.TotalDays < 7) return $"{(int)diff.TotalDays} gün önce";
        return date.ToShortDate();
    }

    /// <summary>Artırma bitişine kalan süre: "2g 4s", "35 dk".</summary>
    public static string ToRemaining(this DateTime date)
    {
        var diff = date - DateTime.Now;
        if (diff.TotalSeconds <= 0) return "Sona erdi";
        if (diff.TotalDays >= 1) return $"{(int)diff.TotalDays}g {diff.Hours}s";
        if (diff.TotalHours >= 1) return $"{(int)diff.TotalHours}s {diff.Minutes}dk";
        return $"{diff.Minutes} dk";
    }
}
